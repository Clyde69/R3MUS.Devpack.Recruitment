using R3MUS.Devpack.Recruitment.Services;
using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace R3MUS.Devpack.Recruitment.Controllers.API
{
    public class GetWalletJournalController : ApiController
    {
        private readonly IApplicantService _applicantService;

        public GetWalletJournalController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        public List<WalletJournalViewModal> Get(long id)
        {
            return _applicantService.GetWalletJournal(id);
        }
    }
}
