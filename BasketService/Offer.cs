using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneySuperMarketTest.Service
{
    public abstract class Offer : IOffer
    {
        public Product OfferProduct { get; set; }
        public int NumberOfOfferProductsForDiscount { get; set; }
        public Product DiscountProduct { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal ApplyDiscount(List<Product> products)
        {
            decimal discountValue = 0;

            var numberOfOfferProductsInBasket = products.Count(x => x.Name == OfferProduct.Name);
            var numberOfDiscountProducts = numberOfOfferProductsInBasket / NumberOfOfferProductsForDiscount;

            var productsToApplyDiscount = products.Where(x => x.Name == DiscountProduct.Name)
                .Take(numberOfDiscountProducts);

            foreach (var product in productsToApplyDiscount)
            {
                discountValue += CalculateDiscount(product);
            }

            return discountValue;
        }
        protected abstract decimal CalculateDiscount(Product products);
    }
    
}
