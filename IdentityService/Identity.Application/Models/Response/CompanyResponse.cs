namespace IdentityService.Application.Models.ResponseModels;

internal class CompanyResponse
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}