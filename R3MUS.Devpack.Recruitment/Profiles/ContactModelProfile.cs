using AutoMapper;
using R3MUS.Devpack.ESI.Models.Character;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.ViewModels;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class ContactModelProfile : Profile
    {
        public ContactModelProfile()
        {
            CreateMap<ContactModel, CharacterContactModel>()
                .ForMember(dest => dest.Character, opt => opt.Ignore());
            CreateMap<ContactModel, CorporationContactModel>()
                .ForMember(dest => dest.Corporation, opt => opt.Ignore());
            CreateMap<ContactModel, AllianceContactModel>()
                .ForMember(dest => dest.Alliance, opt => opt.Ignore());

            CreateMap<CharacterContactModel, ContactViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Character.Name))
                .AfterMap((src, dest) => dest.SetAlertStatus());
            CreateMap<CorporationContactModel, ContactViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Corporation.Name))
                .AfterMap((src, dest) => dest.SetAlertStatus());
            CreateMap<AllianceContactModel, ContactViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Alliance.Name))
                .AfterMap((src, dest) => dest.SetAlertStatus());
        }
    }
}