using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySuperMarketTest.Service
{
    public interface IOffer
    {
        Product OfferProduct { get; set; }
        Product DiscountProduct { get; set; }
        int NumberOfOfferProductsForDiscount { get; set; }
        decimal DiscountValue { get; set; }

        decimal ApplyDiscount(List<Product> products);
    }
}
