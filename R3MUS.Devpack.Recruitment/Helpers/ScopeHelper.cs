using R3MUS.Devpack.ESI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public class ScopeHelper
    {
        public List<string> ApplicantScopes { get; set; }

        public ScopeHelper()
        {
            BuildApplicantScopes();
        }

        private void BuildApplicantScopes()
        {
            ApplicantScopes = new List<string>();
            ApplicantScopes.Add(ESI.Infrastructure.Scopes.Characters.ReadContacts);
            ApplicantScopes.Add(ESI.Infrastructure.Scopes.Characters.ReadStanding);
            ApplicantScopes.Add(ESI.Infrastructure.Scopes.Mail.ReadMail);
        }
    }
}