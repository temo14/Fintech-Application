using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Application.Interfaces;
using Order.Application.Models.Request;
using Order.Application.Models.Response;
using Order.Infrastructure.OrderService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Library;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace Order.Infrastructure.OrderService;

internal class OrderService(
    IDistributedCache distributedCache,
    IOptions<EmailConfig> emailConfigOptions,
    IOptions<ServiceUrls> serviceUrlsOptions,
    ILogger<OrderService> logger) : IOrderService
{
    private EmailConfig _emailConfig = emailConfigOptions.Value;
    private ServiceUrls _serviceUrls = serviceUrlsOptions.Value;
    private static readonly ConcurrentDictionary<string, bool> processingRequests = new();

    public async Task ComputeOrdersAndNotifyAsync(OrderComputeRequest request)
    {
        try
        {
            if (!processingRequests.TryAdd(request.CacheKey, true)) return;

            var delay = Task.Delay(TimeSpan.FromMinutes(2));

            var computedResults = request.Orders.GroupBy(o => o.currency).Select(group => new ComputeOrdersResponse
                (
                    Currency: group.Key.ToString(),
                    TotalAmount: group.Sum(o => o.amount)
                )).ToList();

            var serializedData = JsonSerializer.Serialize(computedResults);

            await delay;

            distributedCache.SetString(request.CacheKey, serializedData, new DistributedCacheEntryOptions());

            var companyInfo = await GetCompanyDataAsync(request.Token, _serviceUrls.IdentityService);

            await SendComputeOrderEmailAsync(companyInfo.Email, companyInfo.CompanyName, computedResults);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"ComputeOrdersAndNotifyAsync error - {ex.Message}");
            throw;
        }
    }

    #region Private

    private async Task<(string Email, string CompanyName)> GetCompanyDataAsync(string token, string url)
    {
        var httpclient = new HttpClient();
        httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpclient.GetAsync($"{url}/company");

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<Result<CompanyResponse>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (!result.IsSuccess)
        {
            logger.LogInformation($"Get company errors - {string.Join(" ,", result.ErrorMessages)}");
        }

        var company = result.Data;

        return (company.Email, company.Name);
    }

    private async Task SendComputeOrderEmailAsync(string email, string companyName, List<ComputeOrdersResponse> data)
    {
        var sendGridClient = new SendGridClient(_emailConfig.ApiKey);
        var from = new EmailAddress(_emailConfig.FromEmail, "OrderSum");
        var to = new EmailAddress(email, companyName);
        var subject = "Computed Orders Summary";
        var plainTextContent = $"Your orders have been computed. Total orders: {data.Count}";

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Order.Infrastructure.OrderService.email_template.html";

        string htmlContent;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            htmlContent = await reader.ReadToEndAsync();
        }

        htmlContent = htmlContent.Replace("[companyName]", companyName);

        var orderRows = string.Join("\n", data.Select(order =>
            $"<tr><td>{order.Currency}</td><td>{order.TotalAmount:N2}</td></tr>"));
        htmlContent = htmlContent.Replace("[orderRows]", orderRows);

        var emailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        await sendGridClient.SendEmailAsync(emailMessage);
    }
    #endregion
}