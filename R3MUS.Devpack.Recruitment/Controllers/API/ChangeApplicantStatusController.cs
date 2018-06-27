using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Services;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class ChangeApplicantStatusController : ApiController
    {
        private readonly IScreeningService _screeningService;

        public ChangeApplicantStatusController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        public void Post(CorporationStatusChangeModel request)
        {
            _screeningService.ChangeStatus(request);
        }
    }
}
