namespace GNBCoreWebAPI.ViewModels
{
    public class TransactionView
    {
        public string Sku { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
    }
}
