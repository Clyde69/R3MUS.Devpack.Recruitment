using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class GetMailController : ApiController
    {
        private readonly IMailService _mailService;

        public GetMailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public List<MailSummaryModel> Get(long id)
        {
            return _mailService.GetMailHeaders(id);
        }

        public MailModel Post(MailModel request)
        {
            return _mailService.GetMailDetail(request);
        }
    }
}
