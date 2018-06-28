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
using R3MUS.Devpack.ESI.Extensions;
using System.Linq;

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
                    ESI.Infrastructure.Scopes.Skills.ReadSkillQueue,
                    ESI.Infrastructure.Scopes.Universe.ReadStructures,
                    ESI.Infrastructure.Scopes.Clones.ReadClones
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

        public ClaimsIdentity SSOReturnForScreeners(HttpContextBase context)
        {
            var owinContext = context.GetOwinContext();
            var userManager = owinContext.GetUserManager<SSOUserManager>();
            var authManager = owinContext.Authentication;

            var endpoint = _esiRepository.GetByName(Resources.ScreenerEndpointName);

            var token = ESI.SingleSignOn.GetTokensFromAuthenticationToken(endpoint.ClientId, endpoint.SecretKey,
                ESI.SingleSignOn.GetAuthorisationCode(context.Request.Url));

            var character = new ESI.Models.Character.Detail(token.AccessToken);
            if (character.GetRolesInCorporation(token.AccessToken).Roles.Contains(Resources.Personnel_Manager))
            {
                var identity = GenerateIdentity(character, token.AccessToken);
                identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), Role.Screener)));

                authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(
                    new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false },
                    identity
                    );

                return identity;
            }
            throw new UnauthorizedAccessException();
        }

        public void LogOut(HttpContextBase context)
        {
            var authManager = context.GetOwinContext().Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            SSOUserManager.SiteUser = null;
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