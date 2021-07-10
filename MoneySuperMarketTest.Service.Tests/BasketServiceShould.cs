using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace MoneySuperMarketTest.Service.Tests
{
    [TestFixture]
    public class BasketServiceShould
    {
        private Product _bread = new Product {Name = "bread", Cost = 1};
        private Product _butter = new Product { Name = "butter", Cost = 0.8m };
        private Product _milk = new Product { Name = "milk", Cost = 1.15m };

        [Test]
        public void CalculateTheCorrectTotalWhenThereAreNoOffersApplicable()
        {
            var products = new List<Product>
            {
                _bread,
                _butter,
                _milk
            };

            var sut = new BasketService();
            var result = sut.CalculateTotal(products);

            result.Should().Be(2.95m);
        }

        [Test]
        public void CalculateTheCorrectTotalWhenThereIsAPercentageOffOffer()
        {
            var products = new List<Product>
            {
                _butter,
                _butter,
                _bread,
                _bread
            };

            var sut = new BasketService();
            var result = sut.CalculateTotal(products);

            result.Should().Be(3.1m);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public class BasketService
    {
        public decimal CalculateTotal(List<Product> products)
        {
            return products.Sum(x => x.Cost);
        }
    }
}
