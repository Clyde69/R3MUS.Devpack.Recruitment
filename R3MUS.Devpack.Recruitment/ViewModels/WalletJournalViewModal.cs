using System;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class WalletJournalViewModal
    {
        public float Amount { get; set; }
        public float Balance { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
    }
}