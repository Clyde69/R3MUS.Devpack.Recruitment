using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Controllers
{
    public class ScreenerController : Controller
    {
        // GET: Screener
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SSORedirect()
        {
            return View();
        }
    }
}