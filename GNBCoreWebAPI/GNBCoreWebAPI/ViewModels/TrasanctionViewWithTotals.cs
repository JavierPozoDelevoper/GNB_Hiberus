namespace GNBCoreWebAPI.ViewModels
{
    public class TrasanctionViewWithTotals
    {
        public List<TransactionView> Transactions { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
