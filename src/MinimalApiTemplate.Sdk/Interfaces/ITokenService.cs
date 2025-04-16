namespace MinimalApiTemplate.Sdk.Interfaces;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}
