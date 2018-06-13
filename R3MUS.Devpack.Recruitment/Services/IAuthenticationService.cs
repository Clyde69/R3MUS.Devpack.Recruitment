using R3MUS.Devpack.Recruitment.Models;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Services
{
    public interface IAuthenticationService
    {
        ActionResult SSOLoginForApplicants();
        ActionResult SSOLoginForScreeners();
        ClaimsIdentity SSOReturnForApplicants(HttpContextBase context);
        void SSOReturnForScreeners();
    }
}