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

        public HomeController(IPageContentService pageContentService)
        {
            _pageContentService = pageContentService;
        }

        public ActionResult Index()
        {
            return View(new PageViewModel { Content = _pageContentService.GetContent() });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}