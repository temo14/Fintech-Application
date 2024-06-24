
namespace IdentityService.Domain.Entities;

public class Company
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string apikey { get; set; }
    public string apisecret { get; set; }
    public DateTime createdat { get; set; } = DateTime.UtcNow;
}