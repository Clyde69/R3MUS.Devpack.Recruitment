using AutoMapper;
using R3MUS.Devpack.ESI.Models.Mail;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Resolvers;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class MailHeaderModelProfile : Profile
    {
        public MailHeaderModelProfile()
        {
            CreateMap<MailHeaderModel, MailSummaryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MailId))
                .ForMember(dest => dest.InOrOut, opt => opt.MapFrom(src => src.SenderId == src.OwnerId ? "OutMail" : "InMail"))
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => new ESI.Models.Character.Detail(src.SenderId).Name))
                .ForMember(dest => dest.Recipients, opt => opt.ResolveUsing<CharactersResolver>())
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
        }
    }
}