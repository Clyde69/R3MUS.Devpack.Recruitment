using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Profiles.Resolvers
{
    public class AuthorisedCorporationResolver : IValueResolver<ESI.Models.Character.Detail, CharacterModel, List<ESI.Models.Corporation.Summary>>
    {
        private readonly IRecruitRepository _recruitRepository;

        public AuthorisedCorporationResolver(IRecruitRepository recruitRepository)
        {
            _recruitRepository = recruitRepository;
        }

        public List<ESI.Models.Corporation.Summary> Resolve(ESI.Models.Character.Detail source, CharacterModel destination, List<ESI.Models.Corporation.Summary> destMember, ResolutionContext context)
        {
            var authorisedCorporations = _recruitRepository.GetRecruitByCharacterId(source.Id).TokenShare.ToList();
            var idList = new IdList() { Ids = authorisedCorporations.Select(s => s.CorporationId).ToList() };
            if (idList.Ids.Count == 0)
            {
                return new List<ESI.Models.Corporation.Summary>();
            }
            return idList.GetCorporationNames().CorporationDetail.ToList();
        }
    }
}