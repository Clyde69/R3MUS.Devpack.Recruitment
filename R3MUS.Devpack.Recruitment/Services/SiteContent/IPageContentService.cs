using System.Runtime.CompilerServices;
using R3MUS.Devpack.Recruitment.Models.Content;

namespace R3MUS.Devpack.Recruitment.Services.SiteContent
{
    public interface IPageContentService
    {
        ContentResponse GetContent([CallerMemberName] string viewName = "", [CallerFilePath] string controllerName = "");
    }
}