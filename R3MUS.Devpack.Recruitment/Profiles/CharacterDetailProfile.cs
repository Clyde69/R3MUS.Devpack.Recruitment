using AutoMapper;
using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class CharacterDetailProfile : Profile
    {
        public CharacterDetailProfile()
        {
            CreateMap<ESI.Models.Character.Detail, CharacterModel>()
                .ForMember(dest => dest.SkillPoints, opt => opt.Ignore())
                .ForMember(dest => dest.AccountStatus, opt => opt.Ignore());
        }
    }
}