using AutoMapper;
using R3MUS.Devpack.ESI.Models.Character;
using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class CorporationModelProfile : Profile
    {
        public CorporationModelProfile()
        {
            CreateMap<HistoricCorporationModel, CorporationModel>()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}