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
        [Test]
        public void CalculateTheCorrectTotalWhenThereAreNoOffersApplicable()
        {
            var products = new List<Product>
            {
                new Product {Name = "bread", Cost = 1},
                new Product {Name = "butter", Cost = 0.8m},
                new Product {Name = "milk", Cost = 1.15m}
            };

            var sut = new BasketService();
            var result = sut.CalculateTotal(products);

            result.Should().Be(2.95m);
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
