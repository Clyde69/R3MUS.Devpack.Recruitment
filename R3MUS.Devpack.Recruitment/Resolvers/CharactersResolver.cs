using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Mail;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.Recruitment.Models;
using System.Collections.Generic;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Resolvers
{
    public class CharactersResolver : IValueResolver<MailHeaderModel, MailSummaryModel, List<string>>
    {
        public List<string> Resolve(MailHeaderModel source, MailSummaryModel destination, List<string> destMember, 
            ResolutionContext context)
        {
            var idList = new IdList() { Ids = source.Recipients.Select(s => s.RecipientId).ToList() };
            var names = idList.GetCharacterNames();
            return names.CharacterDetail.Select(s => s.Name).ToList();
        }
    }
}