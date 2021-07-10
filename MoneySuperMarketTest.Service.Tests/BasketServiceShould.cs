using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

            var sut = new BasketService.BasketService(new List<IOffer>());
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

            var sut = new BasketService.BasketService(new List<IOffer>
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

            var sut = new BasketService.BasketService(new List<IOffer>
            {
                new ValueOffer() { OfferProduct = Milk, DiscountProduct = Milk, NumberOfOfferProductsForDiscount = 3, DiscountValue = 0 }
            });
            var result = sut.CalculateTotal(products);

            result.Should().Be(3.45m);
        }

        [Test]
        public void CalculateTheCorrectTotalWhenThereAreMultipleOffers()
        {
            var products = new List<Product>
            {
                Butter,
                Butter,
                Bread,
                Milk,
                Milk,
                Milk,
                Milk,
                Milk,
                Milk,
                Milk,
                Milk
            };

            var sut = new BasketService.BasketService(new List<IOffer>
            {
                new PercentageOffer() { OfferProduct = Butter, DiscountProduct = Bread, NumberOfOfferProductsForDiscount = 2, DiscountValue = 0.5m }, 
                new ValueOffer() { OfferProduct = Milk, DiscountProduct = Milk, NumberOfOfferProductsForDiscount = 3, DiscountValue = 0 }
            });
            var result = sut.CalculateTotal(products);

            result.Should().Be(9m);
        }
    }

    

    

    
}
