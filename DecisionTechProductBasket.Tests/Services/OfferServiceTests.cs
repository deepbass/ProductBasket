using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTechProductBasket.Services;
using DecisionTechProductBasket.Models;
using Moq;
using DecisionTechProductBasket.Repositories;
using DecisionTechProductBasket.Dtos;

namespace DecisionTechProductBasket.Tests.Controllers
{
    [TestClass]
    public class OfferServiceTests
    {
        private OfferService _offerService = new OfferService(); // stateless so fine


        [TestCategory("Apply All Offers")]
        [TestMethod]
        public void Given_NoProducts_WhenApplyOffers_ReturnZero()
        {
            var testData = new List<Product>();
            var result = _offerService.ApplyOffers(testData);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Given_TwoButterOneBread_WhenApplyOffers_ReturnHalf()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.ApplyOffers(testData);
            Assert.AreEqual(0.50m, result);
        }

        [TestMethod]
        public void Given_FourMilk_WhenApplyOffers_ReturnOnePointOneFive()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 4,
                    Id = Product.MilkId
                }
            };

            var result = _offerService.ApplyOffers(testData);
            Assert.AreEqual(1.15m, result);
        }

        [TestCategory("Apply Buy Two Butter Get One Bread Half Price Tests")]
        [TestMethod]
        public void Given_TwoButterOneBread_WhenBuyTwoButterGetOneBreadHalfPrice_ReturnHalf()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.BuyTwoButterGetOneBreadHalfPrice(testData);
            Assert.AreEqual(0.50m, result);
        }

        [TestMethod]
        public void Given_TwoButterTwoBread_WhenBuyTwoButterGetOneBreadHalfPrice_ReturnHalf()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 2,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.BuyTwoButterGetOneBreadHalfPrice(testData);
            Assert.AreEqual(0.50m, result);
        }
        [TestMethod]
        public void Given_OneButterTwoBread_WhenBuyTwoButterGetOneBreadHalfPrice_ReturnZero()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 2,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 1,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.BuyTwoButterGetOneBreadHalfPrice(testData);
            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void Given_FortyButterOneBread_WhenBuyTwoButterGetOneBreadHalfPrice_ReturnZero()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 40,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.BuyTwoButterGetOneBreadHalfPrice(testData);
            Assert.AreEqual(0.50m, result);
        }

        [TestMethod]
        public void Given_OneButterFortyBread_WhenBuyTwoButterGetOneBreadHalfPrice_ReturnZero()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 40,
                    Id = Product.BreadId
                },
               new Product
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 1,
                    Id = Product.ButterId
                }
            };

            var result = _offerService.BuyTwoButterGetOneBreadHalfPrice(testData);
            Assert.AreEqual(0m, result);
        }

        [TestCategory("Apply Buy Two Butter Get One Bread Half Price Tests")]
        [TestMethod]
        public void Given_FourMilk_WhenBuyThreeMilksGetOneFree_ReturnOnePointOneFive()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 4,
                    Id = Product.MilkId
                }
            };

            var result = _offerService.BuyThreeMilksGetOneFree(testData);
            Assert.AreEqual(1.15m, result);
        }

        [TestMethod]
        public void Given_OneMilk_WhenBuyThreeMilksGetOneFree_ReturnZero()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 1,
                    Id = Product.MilkId
                }
            };

            var result = _offerService.BuyThreeMilksGetOneFree(testData);
            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void Given_FortyMilk_WhenBuyThreeMilksGetOneFree_ReturnElevenPointFifty()
        {
            var testData = new List<Product>
            {
               new Product
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 40,
                    Id = Product.MilkId
                }
            };

            var result = _offerService.BuyThreeMilksGetOneFree(testData);
            Assert.AreEqual(11.50m, result);
        }
    }
}
