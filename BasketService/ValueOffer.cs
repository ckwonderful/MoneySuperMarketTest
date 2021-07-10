using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySuperMarketTest.Service
{
    public class ValueOffer : Offer
    {
        protected override decimal CalculateDiscount(Product product)
        {
            return product.Cost - DiscountValue;
        }
    }
}
