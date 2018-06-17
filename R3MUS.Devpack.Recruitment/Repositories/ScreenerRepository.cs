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
                 w.TokenShare.Select(s => s.CorporationId).Contains(corporationId)).ToList();
        }

        public List<Entities.Recruit> GetAllianceApplicants(long? allianceId)
        {
            return allianceId.HasValue
                ? _databaseContext.Recruits.Where(w =>
                    w.TokenShare.Select(s => s.AllianceId).Contains(allianceId)).ToList()
                : new List<Entities.Recruit>();
        }
    }
}