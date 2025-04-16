using System.Net.Http.Headers;
using MinimalApiTemplate.Sdk.Interfaces;

namespace MinimalApiTemplate.Sdk.Handlers;

public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly ITokenService _tokenService;

    public AuthenticatedHttpClientHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization is null)
        {
            var token = await _tokenService.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}