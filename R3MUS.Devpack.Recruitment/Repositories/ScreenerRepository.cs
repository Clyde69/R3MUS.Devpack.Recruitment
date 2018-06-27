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
            var ignoreStatusInts = new []{ 2, 4, 6 };

            return _databaseContext.Recruits.Where(w =>
                 w.TokenShare.Select(s => s.CorporationId).Contains(corporationId) 
                 && !ignoreStatusInts.Contains(w.TokenShare.First(f => f.CorporationId == corporationId).StatusInt)).ToList();
        }

        public List<Entities.Recruit> GetAllianceApplicants(long? allianceId)
        {
            var ignoreStatusInts = new[] { 2, 4, 6 };

            return allianceId.HasValue
                ? _databaseContext.Recruits.Where(w =>
                 w.TokenShare.Select(s => s.AllianceId).Contains(allianceId)
                 && !ignoreStatusInts.Contains(w.TokenShare.First(f => f.CorporationId == allianceId).StatusInt)).ToList()
                : new List<Entities.Recruit>();
        }

        public void ChangeApplicantStatus(long characterId, long corporationId, int status)
        {
            var utcDate = DateTime.UtcNow;

            var recruit = _databaseContext.Recruits.First(w => w.CharacterId == characterId);
            var history = recruit.TokenShare.First(w => w.CorporationId == corporationId);
            history.StatusInt = status;

            _databaseContext.SaveChanges();
        }
    }
}