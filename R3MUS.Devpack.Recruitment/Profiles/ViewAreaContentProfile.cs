using AutoMapper;
using R3MUS.Devpack.Recruitment.Models.Content;
using R3MUS.Devpack.Recruitment.Repositories.Entities;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class ViewAreaContentProfile : Profile
    {
        public ViewAreaContentProfile()
        {
            CreateMap<ViewArea, ContentAreaResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
        }
    }
}