using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Controllers
{
    public class MailController : Controller
    {
        [Authorize]
        public ActionResult Index(long id)
        {
            return View();
        }
    }
}