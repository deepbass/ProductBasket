using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTechProductBasket;
using DecisionTechProductBasket.Services;
using DecisionTechProductBasket.Models;

namespace DecisionTechProductBasket.Tests.Controllers
{
    [TestClass]
    public class BasketServiceTests
    {
        private OfferService _offerService;

        [TestInitialize]
        public void Initialize()
        {
            _offerService = new OfferService();
        }
        [TestMethod]
        public void Given_NoProducts_WhenGetCurrentBasket_ReturnEmptyBasketWithZeroTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>());

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(0,result.TotalPriceInPounds);
            Assert.AreEqual(0, result.Products.Count);
        }

        [TestMethod]
        public void Given_OneBread_WhenGetCurrentBasket_ReturnBasketWithOneBreadWithEightyPenceTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>() {
                new Product
                {
                    PriceInPounds = 1.00m,
                    Type = Product.ProductType.Bread,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = 1
                }
            });

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(1.00m, result.TotalPriceInPounds);
            Assert.AreEqual(1, result.Products.Count);
            Assert.AreEqual(Product.ProductType.Bread, result.Products[0].Type);
        }

        [TestMethod]
        public void Given_OneBreadOneButterOneMilk_WhenGetCurrentBasket_ReturnBasketWithTwoPoundsNinetyFivePenceTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>() {
                new Product
                {
                    PriceInPounds = 1.00m,
                    Type = Product.ProductType.Bread,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = 1
                },
                new Product
                {
                    Type = Product.ProductType.Butter,
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 1,
                    Id = 2
                },
                new Product
                {
                    Type = Product.ProductType.Milk,
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 1,
                    Id = 3
                }
            });

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(2.95m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_TwoBreadTwoButter_WhenGetCurrentBasket_ReturnBasketWithThreePoundsTenPenceTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>() {
                new Product
                {
                    PriceInPounds = 1.00m,
                    Type = Product.ProductType.Bread,
                    DisplayName = "Bread",
                    Quantity = 2,
                    Id = 1
                },
                new Product
                {
                    Type = Product.ProductType.Butter,
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = 2
                },
            });

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(3.10m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_FourMilk_WhenGetCurrentBasket_ReturnBasketWithThreePoundsFortyFivePenceTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>() {
                new Product
                {
                    Type = Product.ProductType.Milk,
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 4,
                    Id = 1
                }
            });

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(3.45m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_OneBreadTwoButterEightMilk_WhenGetCurrentBasket_ReturnBasketWithninePoundsTotalPrice()
        {
            BasketService service = new BasketService(_offerService, new List<Product>() {
                new Product
                {
                    PriceInPounds = 1.00m,
                    Type = Product.ProductType.Bread,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = 1
                },
                new Product
                {
                    Type = Product.ProductType.Butter,
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = 2
                },
                new Product
                {
                    Type = Product.ProductType.Milk,
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 8,
                    Id = 3
                }
            });

            var result = service.GetCurrentBasket();

            Assert.IsNotNull(result);
            Assert.AreEqual(9.00m, result.TotalPriceInPounds);
        }

    }
}
