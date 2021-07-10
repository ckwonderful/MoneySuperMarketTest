using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySuperMarketTest.Service
{
    public class ValueOffer : Offer
    {
        protected override decimal CalculateSingleDiscount(Product product)
        {
            return product.Cost - DiscountValue;
        }
    }
}
