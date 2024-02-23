namespace Gml.Web.Api.Core.Services;

public interface ISystemService
{
    Task<string> GetPublicKey();
    Task<string> GetPrivateKey();
    Task<string> GetSignature(string data);
}