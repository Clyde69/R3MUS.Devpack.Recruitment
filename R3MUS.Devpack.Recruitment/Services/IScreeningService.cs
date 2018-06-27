using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.ViewModels;

namespace R3MUS.Devpack.Recruitment.Services
{
    public interface IScreeningService
    {
        ScreenerSummaryViewModel GetApplicantList(SSOApplicationUser currentUser);
        ApplicantViewModel GetApplicantDetails(SSOApplicationUser currentUser, long characterId);
        bool ChangeStatus(CorporationStatusChangeModel request);
    }
}