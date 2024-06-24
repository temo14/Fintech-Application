namespace Identity.Application.Models.Response;

public record TokenResponse(string AccessToken, DateTime ExpiresDate);