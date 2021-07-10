using System;
using System.Collections.Generic;
using System.Linq;
using MoneySuperMarketTest.Service;

namespace BasketService
{
    public class BasketService
    {
        private readonly List<IOffer> _offers;

        public BasketService(List<IOffer> offers)
        {
            _offers = offers;
        }

        public decimal CalculateTotal(List<Product> products)
        {
            decimal offerSavings = 0;

            foreach (var offer in _offers)
            {
                offerSavings += offer.ApplyDiscount(products);
            }

            return products.Sum(x => x.Cost) - offerSavings;
        }
    }
}
