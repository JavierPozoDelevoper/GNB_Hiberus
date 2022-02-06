using Microsoft.AspNetCore.Mvc;

namespace GNBCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpGet("/Transaction")]
        public IEnumerable<ViewModels.TransactionView> GetAllTransaction()
        {
            using var context = new Entities.GNBContext();
            var test =  context.Transactions.Select(t => ConvertToView(t)).ToList();
            return test;
        }

        [HttpGet("/Transaction/{sku}")]
        public ViewModels.TrasanctionViewWithTotals GetTransactionsbySkuInEUR(string sku)
        {
            using var context = new Entities.GNBContext();
            Entities.Currency eur = context.Currencies.Where(c => c.CodIso.Equals("EUR")).First();
            List<ViewModels.TransactionView> transactions =  context.Transactions
                .Where(t => t.Sku.Equals(sku))
                .Select(t => ConvertToViewSelectedCurrency(t, eur))
                .ToList();

            ViewModels.TrasanctionViewWithTotals transactionWithtotal = new ViewModels.TrasanctionViewWithTotals
            {
                Transactions = transactions,
                TotalAmount = transactions.Sum(t => t.Amount)
            };
            return transactionWithtotal;
        }

        #region [Private Methods]

        /// <summary>
        /// Convert Database Model Transaction in Visual Model Transaction for API
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private static ViewModels.TransactionView ConvertToView(Entities.Transaction transaction)
        {
            using var context = new Entities.GNBContext();
            return new ViewModels.TransactionView()
            {
                Sku = transaction.Sku,
                Amount = transaction.Amount,
                Currency = context.Currencies.First( c => c.Idcurrency.Equals(transaction.Idcurrency)).CodIso
            };
        }

        private static ViewModels.TransactionView ConvertToViewSelectedCurrency(Entities.Transaction transaction, Entities.Currency currency)
        {
            decimal ratio = RateController.GetRateValue(transaction.Idcurrency, currency.Idcurrency);
            if(ratio == 0)
            {
                throw new Exception($"No Exist posible conversion between {transaction.IdcurrencyNavigation.CodIso} and {currency.CodIso}");
            }

            return new ViewModels.TransactionView()
            {
                Sku = transaction.Sku,
                Amount = ratio * transaction.Amount,
                Currency = currency.CodIso
            };
        }

        #endregion
    }
}
