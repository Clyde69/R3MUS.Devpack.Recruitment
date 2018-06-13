using R3MUS.Devpack.ESI.Extensions;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class SearchCorporationController : ApiController
    {
        public ESI.Models.Corporation.Summary Get(string corporationName)
        {
            return corporationName.GetCorporationDetailsByName();
        }
    }
}
