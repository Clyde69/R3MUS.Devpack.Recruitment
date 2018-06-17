using R3MUS.Devpack.Recruitment.ViewModels;
using System.Collections.Generic;

namespace R3MUS.Devpack.Recruitment.Services
{
    public interface IApplicantService
    {
        List<ContactViewModel> GetContacts(long id);
        ApplicantViewModel GetCharacterViewModel(long id);
        List<WalletJournalViewModal> GetWalletJournal(long id);
        List<WalletTransactionViewModel> GetWalletTransactions(long id);
    }
}