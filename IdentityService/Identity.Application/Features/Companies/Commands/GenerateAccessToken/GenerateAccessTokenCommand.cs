using Identity.Application.Models.Response;
using MediatR;
using Shared.Library;

namespace Identity.Application.Features.Companies.Commands.GenerateAccessToken;

public record GenerateAccessTokenCommand(
    string ApiKey,
    string ApiSecret)
    : IRequest<Result<TokenResponse>>;