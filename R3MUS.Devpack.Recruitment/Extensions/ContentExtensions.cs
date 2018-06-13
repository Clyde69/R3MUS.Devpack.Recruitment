using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace R3MUS.Devpack.Recruitment.Extensions
{
    public static class ContentExtensions
    {
        public static HtmlString Content(this HtmlHelper helper)
        {
            var content = ((PageViewModel)helper.ViewDataContainer.ViewData.Model).Content;

            var areaId = helper.GetCurrentElementId();

            if (string.IsNullOrEmpty(areaId))
            {
                return new HtmlString("<h2>No id has been set for this element</h2>");
            }

            if (content.Areas.FirstOrDefault(w => w.Name.Equals(areaId)) == null
                || string.IsNullOrWhiteSpace(content.Areas.FirstOrDefault(w => w.Name.Equals(areaId)).Content)
                || string.IsNullOrEmpty(content.Areas.FirstOrDefault(w => w.Name.Equals(areaId)).Content))
            {
                return new HtmlString("<h2>There is no content defined for this area</h2>");
            }
            return new HtmlString(content.Areas.FirstOrDefault(w => w.Name.Equals(areaId)).Content);
        }

        public static string GetCurrentElementId(this HtmlHelper helper)
        {
            var reader = new HtmlTextWriter(helper.ViewContext.Writer).InnerWriter;
            var lastElement = new HtmlTextWriter(helper.ViewContext.Writer).InnerWriter.ToString()
                .Split(new string[] { "<" }, StringSplitOptions.None).ToList().Last();

            if (lastElement.IndexOf(@"id") == -1)
            {
                return string.Empty;
            }

            var areaId = lastElement.Substring(lastElement.IndexOf(@"id"));
            areaId = areaId.Substring(areaId.IndexOf("\"") + 1);
            areaId = areaId.Substring(0, areaId.IndexOf("\""));

            return areaId;
        }
    }
}