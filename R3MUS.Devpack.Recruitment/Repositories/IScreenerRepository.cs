using System.Collections.Generic;
using R3MUS.Devpack.Recruitment.Repositories.Entities;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public interface IScreenerRepository
    {
        List<Recruit> GetAllianceApplicants(long? allianceId);
        List<Recruit> GetCorporationApplicants(long corporationId);
        void ChangeApplicantStatus(long characterId, long corporationId, int status);
    }
}