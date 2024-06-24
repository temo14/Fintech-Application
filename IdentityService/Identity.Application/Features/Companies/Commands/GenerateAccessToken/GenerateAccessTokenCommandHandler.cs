using Identity.Application.Models.Response;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Interfaces;
using MediatR;
using Shared.Library;

namespace Identity.Application.Features.Companies.Commands.GenerateAccessToken;

internal class GenerateAccessTokenCommandHandler(
    IAuthService authService,
    ICompanyRepository repository)
    : IRequestHandler<GenerateAccessTokenCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var company = await repository.GetByApiKeyAndSecretAsync(request.ApiKey, request.ApiSecret);

        if (company == null)
            throw new Exception("Invalid API credentials");

        var generatedAccessToken = authService.GenerateAccessToken(company.id, out var accessTokenExpiresAt);

        return Result<TokenResponse>.Success(new TokenResponse(generatedAccessToken, accessTokenExpiresAt));
    }
}