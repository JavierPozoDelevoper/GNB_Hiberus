using Microsoft.AspNetCore.Mvc;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace GNBCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RateController : ControllerBase
    {

        [HttpGet(Name = "GetAllRates")]
        public IEnumerable<ViewModels.RateView> GetAllRates()
        {
            using var context = new Entities.GNBContext();
            return context.Rates.Select(rate => new ViewModels.RateView()
            {
                From = rate.IdcurrencyFromNavigation.CodIso,
                To = rate.IdcurrencyToNavigation.CodIso,
                Rate = rate.Value
            }).ToList();                
        }


        public static decimal GetRateValue(string idCurrencyFrom, string idCurrencyTo, List<string> excludeCurrencies = null)
        {
            using var context = new Entities.GNBContext();

            //Comprobamos si hay relacion
            if (IsSameCurrency(idCurrencyFrom, idCurrencyTo)){ return 1; }

            //Relacion Directa
            Entities.Rate directRate = GetDirectRate(idCurrencyFrom, idCurrencyTo);
            if (directRate != null) { return directRate.Value;  }

            //Relación indirecta
            excludeCurrencies ??= new List<string>();
            excludeCurrencies.Add(idCurrencyFrom);

            decimal tempRatio = 0;
            GetInDirectRate(idCurrencyFrom, excludeCurrencies).ForEach(rate => 
            {
                decimal tempRate = GetRateValue(rate.IdcurrencyTo, idCurrencyTo, excludeCurrencies);
                if (tempRate != 0)
                {
                    tempRatio = tempRate * rate.Value;
                    return;
                };
            });

            return tempRatio;


        }

        private static bool IsSameCurrency(string idCurrencyFrom, string idCurrencyTo)
        {
            return idCurrencyFrom.Equals(idCurrencyTo);
        }

        private static Entities.Rate GetDirectRate(string idCurrencyFrom, string idCurrencyTo)
        {
            using var context = new Entities.GNBContext();
            return context.Rates.Where(c => c.IdcurrencyFrom.Equals(idCurrencyFrom)
                 & c.IdcurrencyTo.Equals(idCurrencyTo)).FirstOrDefault();
        }

        private static List<Entities.Rate> GetInDirectRate( string idCurrencyFrom, List<string> excludeCurrencies)
        {
            using var context = new Entities.GNBContext();
            return context.Rates.Where(c => c.IdcurrencyFrom.Equals(idCurrencyFrom)                
                && !excludeCurrencies.Any(s => s.Equals(c.IdcurrencyTo))).ToList();                
        }

    }
}
