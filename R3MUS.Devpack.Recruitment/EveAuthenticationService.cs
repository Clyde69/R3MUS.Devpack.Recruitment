using Microsoft.AspNet.Identity;
using R3MUS.Devpack.ESI;
using R3MUS.Devpack.ESI.Models.Verification;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment
{
    public class EveAuthenticationService : IEveAuthenticationService
    {
        public ActionResult SSOLogin(string redirectUrl, string clientId)
        {
            return SingleSignOn.SignOn(redirectUrl, clientId, new List<string>());
        }

        public TokenResponse GetAccessTokenFromAuthenticationToken(string token, string clientId, string appKey)
        {
            return SingleSignOn.GetTokensFromAuthenticationToken(clientId, appKey, token);
        }

        public ClaimsIdentity GenerateIdentity(string authToken)
        {
            var toon = new ESI.Models.Character.Detail(authToken);

            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, toon.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));

            identity.AddClaim(new Claim(ClaimTypes.Name, toon.Name));

            return identity;
        }
    }
}