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

        public string Post(CorporationAuthorisationModel request)
        {
            request.AllianceId = new ESI.Models.Corporation.Detail(request.CorporationId).AllianceId;
            return _recruitRepository.AddCorporation(request).ToString();
        }

        //public IHttpActionResult Post(CorporationAuthorisationModel request)
        //{
        //    try
        //    {
        //        return Ok(_recruitRepository.AddCorporation(request).ToString());
        //    }
        //    catch
        //    {
        //        return StatusCode(System.Net.HttpStatusCode.Conflict);
        //    }
        //}

        public void Delete(CorporationAuthorisationModel request)
        {
            _recruitRepository.DeleteCorporation(request);
        }
    }
}
