using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Extensions
{
    public static class IdListExtensions
    {
        public static List<ESIBasicEntity> GetEntityNames(this IdList me)
        {
            var characters = me.GetCharacterNames().CharacterDetail;
            var corporationIdList = new IdList()
            {
                Ids = characters
                    .Where(w => w.Name.Equals("Character Not Found")).Select(w => w.Id).ToList()
            };
            var corporations = corporationIdList.GetCorporationNames().CorporationDetail;

            characters = characters.Where(w => !w.Name.Equals("Character Not Found")).ToList();

            var result = new List<ESIBasicEntity>();

            characters.ForEach(character =>  result.Add(new ESIBasicEntity() { Id = character.Id, Name = character.Name, Type = "character" }));
            corporations.ForEach(corporation => result.Add(new ESIBasicEntity() { Id = corporation.Id, Name = corporation.Name, Type = "corporation" }));

            return result;
        }
    }
}