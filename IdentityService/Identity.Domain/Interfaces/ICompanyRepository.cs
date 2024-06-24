using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<Company> AddCompanyAsync(Company company);
    Task<Company> GetCompanyByIdAsync(int id);
    Task<Company> GetByApiKeyAndSecretAsync(string apiKey, string apiSecret);
}