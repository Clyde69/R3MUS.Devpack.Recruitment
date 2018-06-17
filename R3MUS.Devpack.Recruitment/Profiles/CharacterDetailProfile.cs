using AutoMapper;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Profiles.Resolvers;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class CharacterDetailProfile : Profile
    {
        public CharacterDetailProfile()
        {
            CreateMap<ESI.Models.Character.Detail, CharacterModel>()
                .ForMember(dest => dest.SkillPoints, opt => opt.Ignore())
                .ForMember(dest => dest.AccountStatus, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorisedCorporations, opt => opt.ResolveUsing<AuthorisedCorporationResolver>());
        }
    }
}