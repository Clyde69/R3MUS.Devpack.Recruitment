using R3MUS.Devpack.Recruitment.Helpers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using R3MUS.Devpack.Recruitment.Repositories;
using R3MUS.Devpack.Recruitment.Enums;
using R3MUS.Devpack.Recruitment.Properties;

namespace R3MUS.Devpack.Recruitment.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IESIEndpointRepository _esiRepository;
        private readonly IRecruitRepository _recruitRepository;
        
        public AuthenticationService(IESIEndpointRepository esiRepository, IRecruitRepository recruitRepository)
        {
            _esiRepository = esiRepository;
            _recruitRepository = recruitRepository;
        }

        public ActionResult SSOLoginForApplicants()
        {
            var endpoint = _esiRepository.GetByName(Resources.ApplicantEndpointName);

            return ESI.SingleSignOn.SignOn(endpoint.CallbackUrl, endpoint.ClientId,
                new List<string>() {
                    ESI.Infrastructure.Scopes.Mail.ReadMail,
                    ESI.Infrastructure.Scopes.Characters.ReadContacts,
                    ESI.Infrastructure.Scopes.Characters.ReadStanding,
                    ESI.Infrastructure.Scopes.Wallet.ReadCharacterWallet,
                    ESI.Infrastructure.Scopes.Skills.ReadSkills,
                    ESI.Infrastructure.Scopes.Skills.ReadSkillQueue
                });
        }

        public ActionResult SSOLoginForScreeners()
        {
            var endpoint = _esiRepository.GetByName(Resources.ScreenerEndpointName);

            return ESI.SingleSignOn.SignOn(endpoint.CallbackUrl, endpoint.ClientId,
                new List<string>() { ESI.Infrastructure.Scopes.Characters.ReadCorporationRoles });
        }

        public ClaimsIdentity SSOReturnForApplicants(HttpContextBase context)
        {
            var owinContext = context.GetOwinContext();
            var userManager = owinContext.GetUserManager<SSOUserManager>();
            var authManager = owinContext.Authentication;

            var endpoint = _esiRepository.GetByName(Resources.ApplicantEndpointName);

            var token = ESI.SingleSignOn.GetTokensFromAuthenticationToken(endpoint.ClientId, endpoint.SecretKey,
                ESI.SingleSignOn.GetAuthorisationCode(context.Request.Url));

            var character = new ESI.Models.Character.Detail(token.AccessToken);
            var identity = GenerateIdentity(character, token.AccessToken);
            identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), Role.Applicant)));

            authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(
                new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false },
                identity
                );

            _recruitRepository.AddOrUpdateToken(token.RefreshToken, character.Id);

            return identity;
        }

        public void SSOReturnForScreeners()
        {

        }

        private ClaimsIdentity GenerateIdentity(ESI.Models.Character.Detail character, string token)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, character.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));

            identity.AddClaim(new Claim(ClaimTypes.Name, character.Name));
            identity.AddClaim(new Claim(ClaimTypes.Hash, token));

            return identity;
        }
    }
}