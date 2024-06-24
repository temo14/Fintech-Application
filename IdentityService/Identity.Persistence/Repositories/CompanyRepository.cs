using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.Repositories;

internal class CompanyRepository(
    AppDbContext context
    ) : ICompanyRepository
{
    public async Task<Company> GetCompanyByIdAsync(int id)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.id == id);
    }

    public async Task<Company> AddCompanyAsync(Company company)
    {

        context.Companies.Add(company);
        await context.SaveChangesAsync();

        return company;
    }

    public async Task<Company> GetByApiKeyAndSecretAsync(string apiKey, string apiSecret)
    {
        return await context.Companies.FirstOrDefaultAsync(x => x.apikey == apiKey && x.apisecret == apiSecret);
    }
}