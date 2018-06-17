using AutoMapper;
using R3MUS.Devpack.ESI.Models.Wallet;
using R3MUS.Devpack.Recruitment.ViewModels;

namespace R3MUS.Devpack.Recruitment.Profiles
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<WalletJournalEntry, WalletJournalViewModal>();

            CreateMap<WalletTransactionEntry, WalletTransactionViewModel>()
                .ForMember(dest => dest.ClientName, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.ItemTypeName, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.BuySell = src.IsBuy ? "Buy" : "Sell");
        }
    }
}