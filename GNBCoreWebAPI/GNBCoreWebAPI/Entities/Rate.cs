using System;
using System.Collections.Generic;

namespace GNBCoreWebAPI.Entities
{
    public partial class Rate
    {
        public string Idrate { get; set; } = null!;
        public string IdcurrencyFrom { get; set; } = null!;
        public string IdcurrencyTo { get; set; } = null!;
        public decimal Value { get; set; }

        public virtual Currency IdcurrencyFromNavigation { get; set; } = null!;
        public virtual Currency IdcurrencyToNavigation { get; set; } = null!;
    }
}
