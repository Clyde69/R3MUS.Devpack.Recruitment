using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories;
using System.Collections.Generic;
using System.Linq;
using R3MUS.Devpack.ESI.Models.Character;
using System;

namespace R3MUS.Devpack.Recruitment.Profiles.Resolvers
{
    public class AuthorisedCorporationResolver : IValueResolver<ESI.Models.Character.Detail, CharacterModel, List<AuthorisedCorporationModel>>
    {
        private readonly IRecruitRepository _recruitRepository;

        public AuthorisedCorporationResolver(IRecruitRepository recruitRepository)
        {
            _recruitRepository = recruitRepository;
        }

        public List<AuthorisedCorporationModel> Resolve(ESI.Models.Character.Detail source, CharacterModel destination, List<AuthorisedCorporationModel> destMember, ResolutionContext context)
        {
            var recruit = _recruitRepository.GetRecruitByCharacterId(source.Id);
            var authorisedCorporations = recruit.TokenShare;
            var idList = new IdList() { Ids = authorisedCorporations.Select(s => s.CorporationId).ToList() };
            if (idList.Ids.Count == 0)
            {
                return new List<AuthorisedCorporationModel>();
            }
            var result = new List<AuthorisedCorporationModel>();
            idList.GetCorporationNames().CorporationDetail.ToList().ForEach(f => {
                result.Add(new AuthorisedCorporationModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Status = _recruitRepository.GetCurrentStatus(new CorporationAuthorisationModel()
                    {
                        CorporationId = f.Id,
                        RecruitId = recruit.Id
                    }).Status.ToString()
                });
            });

            return result;
        }
    }
}