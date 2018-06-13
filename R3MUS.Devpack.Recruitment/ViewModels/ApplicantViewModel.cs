using R3MUS.Devpack.ESI.Models.Mail;
using R3MUS.Devpack.Recruitment.Models;
using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class ApplicantViewModel
    {
        public CharacterModel Applicant { get; set; }
        public List<CharacterContactModel> Contacts { get; set; }
    }
}