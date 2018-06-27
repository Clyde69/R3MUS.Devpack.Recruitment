using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IRecruitRepository
    {
        void AddOrUpdateToken(string token, long characterId);
        string GetRefreshTokenForApplicant(long characterId);
        Enums.ApplicationStatus AddCorporation(CorporationAuthorisationModel request);
        void DeleteCorporation(CorporationAuthorisationModel request);
        Recruit GetRecruitByCharacterId(long characterId);
        List<History> GetCurrentStatuses(int recruitId);
        History GetCurrentStatus(CorporationAuthorisationModel request);
    }
}