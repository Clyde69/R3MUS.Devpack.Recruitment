using R3MUS.Devpack.Recruitment.Enums;
using R3MUS.Devpack.Recruitment.Extensions;
using R3MUS.Devpack.Recruitment.Helpers;
using R3MUS.Devpack.Recruitment.Services;
using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Controllers
{
    public class ScreenerController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISSOUserManager _userManager;
        private readonly IScreeningService _screeningService;

        private const string _applicantEndpointName = "Applicant";
        private const string _screenerEndpointName = "Screener";
        
        public ScreenerController(IAuthenticationService authenticationService, ISSOUserManager userManager, 
            IScreeningService screeningService)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _screeningService = screeningService;
        }

        public ActionResult Login()
        {
            return _authenticationService.SSOLoginForScreeners();
        }

        public ActionResult SSORedirect()
        {
            try
            {
                var identity = _authenticationService.SSOReturnForScreeners(HttpContext);
                _userManager.FindByIdentity(identity);
            }
            catch(UnauthorizedAccessException)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ViewApplicants");
        }

        [Authorize]
        public ActionResult ViewApplicants()
        {
            return View(new ScreenerSummaryPageViewModel()
            {
                ApplicationSummary = _screeningService.GetApplicantList(SSOUserManager.SiteUser),
                ErrorMessage = TempData["ErrorMessage"] != null 
                    ? TempData["ErrorMessage"].ToString()
                    : ErrorMessageHelper.GetNonErrorMessage()
            });
        }

        [Authorize]
        public ActionResult ViewApplicant(long id)
        {
            ViewBag.Title = "Review Applicant";
            try
            {
                return View("ViewApplicant", new ApplicantPageViewModel()
                {
                    Applicant = _screeningService.GetApplicantDetails(SSOUserManager.SiteUser, id),
                    ViewMode = Enums.Role.Screener,
                    AllStatusValues = EnumExtensions.GetDisplayNames<ApplicationStatus>()
                });
            }
            catch(UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "You are not authorised to view that applicant";
                return RedirectToAction("ViewApplicants");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ViewApplicants");
            }
        }
    }
}