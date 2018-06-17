using R3MUS.Devpack.Recruitment.Services;
using R3MUS.Devpack.Recruitment.Services.SiteContent;
using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageContentService _pageContentService;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IPageContentService pageContentService, IAuthenticationService authenticationService)
        {
            _pageContentService = pageContentService;
            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View(new PageViewModel { Content = _pageContentService.GetContent() });
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            _authenticationService.LogOut(HttpContext);

            return RedirectToAction("Index");
        }
    }
}