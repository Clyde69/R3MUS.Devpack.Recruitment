using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
