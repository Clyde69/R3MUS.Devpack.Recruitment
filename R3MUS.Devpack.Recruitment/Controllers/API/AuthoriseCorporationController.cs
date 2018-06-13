using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class AuthoriseCorporationController : ApiController
    {
        private readonly IRecruitRepository _recruitRepository;

        public AuthoriseCorporationController(IRecruitRepository recruitRepository)
        {
            _recruitRepository = recruitRepository;
        }

        public void Post(CorporationAuthorisationModel request)
        {
            _recruitRepository.AddCorporation(request);
        }

        public void Delete(CorporationAuthorisationModel request)
        {
            _recruitRepository.DeleteCorporation(request);
        }
    }
}
