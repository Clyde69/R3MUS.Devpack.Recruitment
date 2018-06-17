using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class ScreenerSummaryViewModel
    {
        public List<ESI.Models.Character.Summary> CorporateApplications { get; set; }
        public List<ESI.Models.Character.Summary> AllianceApplications { get; set; }
    }
}