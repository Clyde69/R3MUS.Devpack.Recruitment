using R3MUS.Devpack.Recruitment.Services;
using R3MUS.Devpack.Recruitment.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class GetContactsController : ApiController
    {
        private readonly IApplicantService _applicantService;

        public GetContactsController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        public List<ContactViewModel> Get(long id)
        {
            return _applicantService.GetContacts(id);
        }
    }
}
