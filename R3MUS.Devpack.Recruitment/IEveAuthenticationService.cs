using R3MUS.Devpack.ESI.Models.Verification;
using System.Security.Claims;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment
{
    public interface IEveAuthenticationService
    {
        ClaimsIdentity GenerateIdentity(string authToken);
        TokenResponse GetAccessTokenFromAuthenticationToken(string token, string clientId, string appKey);
        ActionResult SSOLogin(string redirectUrl, string clientId);
    }
}