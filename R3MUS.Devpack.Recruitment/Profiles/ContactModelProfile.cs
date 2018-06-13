using AutoMapper;
using R3MUS.Devpack.ESI.Models.Character;
using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class ContactModelProfile : Profile
    {
        public ContactModelProfile()
        {
            CreateMap<ContactModel, CharacterContactModel>()
                .ForMember(dest => dest.Character, opt => opt.Ignore());
        }
    }
}