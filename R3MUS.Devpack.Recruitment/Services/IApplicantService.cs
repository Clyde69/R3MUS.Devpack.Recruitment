using R3MUS.Devpack.Recruitment.ViewModels;

namespace R3MUS.Devpack.Recruitment.Services
{
    public interface IApplicantService
    {
        ApplicantViewModel GetCharacterViewModel(long id);
    }
}