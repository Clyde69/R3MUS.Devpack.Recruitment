using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.Recruitment.Models.Content
{
    public class ContentAreaResponse
    {
        public string Name { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}