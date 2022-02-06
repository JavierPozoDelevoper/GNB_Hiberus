using System;
using System.Collections.Generic;

namespace GNBCoreWebAPI.Entities
{
    public partial class Currency
    {
        public Currency()
        {
            RateIdcurrencyFromNavigations = new HashSet<Rate>();
            RateIdcurrencyToNavigations = new HashSet<Rate>();
            Transactions = new HashSet<Transaction>();
        }

        public string Idcurrency { get; set; } = null!;
        public string CodIso { get; set; } = null!;

        public virtual ICollection<Rate> RateIdcurrencyFromNavigations { get; set; }
        public virtual ICollection<Rate> RateIdcurrencyToNavigations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
