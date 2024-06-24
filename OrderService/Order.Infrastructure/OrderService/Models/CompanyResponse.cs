using System.Text.Json.Serialization;

public class CompanyResponse
{
    [JsonPropertyName("companyId")]
    public int CompanyId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    [JsonPropertyName("apiSecret")]
    public string ApiSecret { get; set; }
}
