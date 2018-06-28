using R3MUS.Devpack.Recruitment.Enums;
using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class ApplicantPageViewModel : PageViewModel
    {
        public ApplicantViewModel Applicant { get; set; }
        public Role ViewMode { get; set; }
        public Dictionary<ApplicationStatus, string> AllStatusValues { get; set; }
    }
}