﻿using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.Recruitment.Helpers;
using R3MUS.Devpack.Recruitment.Repositories;
using R3MUS.Devpack.Recruitment.Services;
using R3MUS.Devpack.Recruitment.ViewModels;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISSOUserManager _userManager;
        private readonly IRecruitRepository _recruitRepository;
        private readonly IApplicantService _applicantService;

        private const string _endpointName = "Applicant";

        public ApplicantController(IAuthenticationService authenticationService, ISSOUserManager userManager,
            IRecruitRepository recruitRepository, IApplicantService applicantService)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _recruitRepository = recruitRepository;
            _applicantService = applicantService;
        }

        public ActionResult Apply()
        {
            return _authenticationService.SSOLoginForApplicants();
        }

        public ActionResult SSORedirect()
        {
            var identity = _authenticationService.SSOReturnForApplicants(HttpContext);
 
            _userManager.FindByIdentity(identity);

            return RedirectToAction("FinaliseOptions");
        }

        [Authorize]
        public ActionResult FinaliseOptions()
        {
            return View(new ApplicantPageViewModel() { Applicant = _applicantService.GetCharacterViewModel(SSOUserManager.SiteUser.Character.Id) });
        }
    }
}