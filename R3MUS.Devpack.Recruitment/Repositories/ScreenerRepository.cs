using System;
using System.Collections.Generic;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class ScreenerRepository : IScreenerRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ScreenerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public List<Entities.Recruit> GetCorporationApplicants(long corporationId)
        {
            return _databaseContext.Recruits.Where(w =>
                 w.TokenShare.Select(s => s.CorporationId).Contains(corporationId)
                    && w.History.OrderByDescending(o => o.ActionDate).FirstOrDefault().StatusInt != (int)Enums.ApplicationStatus.Accepted).ToList();
        }

        public List<Entities.Recruit> GetAllianceApplicants(long? allianceId)
        {
            return allianceId.HasValue
                ? _databaseContext.Recruits.Where(w =>
                    w.TokenShare.Select(s => s.AllianceId).Contains(allianceId)
                    && w.History.OrderByDescending(o => o.ActionDate).FirstOrDefault().StatusInt == (int)Enums.ApplicationStatus.AllianceReview).ToList()
                : new List<Entities.Recruit>();
        }

        public void ChangeApplicantStatus(long characterId, long corporationId, int status)
        {
            var utcDate = DateTime.UtcNow;
            var recruit = _databaseContext.Recruits.First(w => w.CharacterId == characterId);
            var history = recruit.History.First(w => w.CorporationId == corporationId);
            history.StatusInt = status;
            history.ActionDate = utcDate;
            _databaseContext.SaveChanges();
        }
    }
}