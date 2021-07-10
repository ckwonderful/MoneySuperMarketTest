using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using NUnit.Framework;

namespace MoneySuperMarketTest.Service.Tests
{
    [TestFixture]
    public class BasketServiceShould
    {
        private static Product Bread => new Product {Name = "bread", Cost = 1};
        private static Product Butter => new Product { Name = "butter", Cost = 0.8m };

        private static Product Milk => new Product { Name = "milk", Cost = 1.15m };

        [Test]
        public void CalculateTheCorrectTotalWhenThereAreNoOffersApplicable()
        {
            var products = new List<Product>
            {
                Bread,
                Butter,
                Milk
            };

            var sut = new BasketService(new List<IOffer>());
            var result = sut.CalculateTotal(products);

            result.Should().Be(2.95m);
        }

        [Test]
        public void CalculateTheCorrectTotalWhenThereIsAPercentageOffOffer()
        {
            var products = new List<Product>
            {
                Butter,
                Butter,
                Bread,
                Bread
            };

            var sut = new BasketService(new List<IOffer>
            {
                new PercentageOffer() { OfferProduct = Butter, DiscountProduct = Bread, NumberOfOfferProductsForDiscount = 2, DiscountValue = 0.5m }
            });
            var result = sut.CalculateTotal(products);

            result.Should().Be(3.1m);
        }

        [Test]
        public void CalculateTheCorrectTotalWhenThereIsAFreeProductOffer()
        {
            var products = new List<Product>
            {
                Milk,
                Milk,
                Milk,
                Milk
            };

            var sut = new BasketService(new List<IOffer>
            {
                new ValueOffer() { OfferProduct = Milk, DiscountProduct = Milk, NumberOfOfferProductsForDiscount = 3, DiscountValue = 0 }
            });
            var result = sut.CalculateTotal(products);

            result.Should().Be(3.45m);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

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

    public class PercentageOffer : Offer
    {
        protected override decimal CalculateDiscount(Product product)
        {
            return product.Cost * DiscountValue;
        }
        
    }

    public class ValueOffer : Offer
    {
        protected override decimal CalculateDiscount(Product product)
        {
            return product.Cost - DiscountValue;
        }
    }

    public abstract class Offer: IOffer
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

    public interface IOffer
    {
        Product OfferProduct { get; set; }
        Product DiscountProduct { get; set; }
        int NumberOfOfferProductsForDiscount { get; set; }
        decimal DiscountValue { get; set; }

        decimal ApplyDiscount(List<Product> products);
    }
}
