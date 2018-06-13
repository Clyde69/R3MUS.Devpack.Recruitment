using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IRecruitRepository
    {
        void AddOrUpdateToken(string token, long characterId);
        string GetRefreshTokenForApplicant(long characterId);
        void AddCorporation(CorporationAuthorisationModel request);
        void DeleteCorporation(CorporationAuthorisationModel request);
    }
}