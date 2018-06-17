using System;

namespace R3MUS.Devpack.Recruitment.ViewModels
{
    public class WalletTransactionViewModel
    {
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime Timestamp { get; set; }
        public string BuySell { get; set; }
        public int Quantity { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public float UnitPrice { get; set; }
    }
}