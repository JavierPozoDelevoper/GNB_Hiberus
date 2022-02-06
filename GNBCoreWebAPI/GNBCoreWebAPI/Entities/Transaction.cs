using System;
using System.Collections.Generic;

namespace GNBCoreWebAPI.Entities
{
    public partial class Transaction
    {
        public string Idtransaction { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public string Idcurrency { get; set; } = null!;
        public decimal Amount { get; set; }

        public virtual Currency IdcurrencyNavigation { get; set; } = null!;
    }
}
