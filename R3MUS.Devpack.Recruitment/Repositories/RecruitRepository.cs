using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Repositories
{
    public class RecruitRepository : IRecruitRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RecruitRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddOrUpdateToken(string token, long characterId)
        {
            if (!_databaseContext.Recruits.Any(w => w.CharacterId == characterId))
            {
                _databaseContext.Recruits.Add(new Entities.Recruit()
                {
                    CharacterId = characterId,
                    History = new List<Entities.History>()
                    //{
                    //    new History(){
                    //        ActionDate = DateTime.Now,
                    //        Status = Enums.ApplicationStatus.Applied
                    //    }
                    //}
                });
                _databaseContext.SaveChanges();
            }

            if (_databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData == null)
            {
                _databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData = new List<TokenData>();
            }
            if (_databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData.Any())
            {
                _databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData
                    .First().RefreshToken = token;
            }
            else
            {
                _databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData
                    .Add(new Entities.TokenData() { RefreshToken = token });
            }
            _databaseContext.SaveChanges();
        }
        
        public string GetRefreshTokenForApplicant(long characterId)
        {
            var recruits = _databaseContext.Recruits;
            var recruit = recruits.First(w => w.CharacterId == characterId);
            return _databaseContext.Recruits.First(w => w.CharacterId == characterId).TokenData.First().RefreshToken;
        }

        public Enums.ApplicationStatus AddCorporation(CorporationAuthorisationModel request)
        {
            _databaseContext.Recruits.First(w => w.CharacterId == request.RecruitId).TokenShare
                .Add(new TokenShare() { CorporationId = request.CorporationId  });

            var utcNow = DateTime.Now;
            var toon = _databaseContext.Recruits.First(w => w.CharacterId == request.RecruitId);

            _databaseContext.Recruits.First(w => w.CharacterId == request.RecruitId).History
                .Add(new History() { Status = Enums.ApplicationStatus.Applied, CorporationId = request.CorporationId, ActionDate = utcNow });

            _databaseContext.SaveChanges();
            return Enums.ApplicationStatus.Applied;
        }        

        public List<History> GetCurrentStatuses(int recruitId)
        {
            return _databaseContext.Recruits.First(w => w.CharacterId == recruitId).History
                .GroupBy(g => g.CorporationId).Select(s => s.OrderByDescending(w => w.ActionDate).FirstOrDefault()).ToList();
        }

        public History GetCurrentStatus(CorporationAuthorisationModel request)
        {
            return _databaseContext.Historys.Where(w => w.RecruitId == request.RecruitId && w.CorporationId == request.CorporationId)
                .OrderByDescending(o => o.ActionDate).FirstOrDefault();
        }

        public void DeleteCorporation(CorporationAuthorisationModel request)
        {
            var recruit = _databaseContext.Recruits.First(w => w.CharacterId == request.RecruitId);
            var tokenShare = recruit.TokenShare.First(w => w.CorporationId == request.CorporationId);
            _databaseContext.Entry(tokenShare).State = EntityState.Deleted;
            _databaseContext.SaveChanges();
        }

        public Recruit GetRecruitByCharacterId(long characterId)
        {
            return _databaseContext.Recruits.First(w => w.CharacterId == characterId);
        }
    }
}