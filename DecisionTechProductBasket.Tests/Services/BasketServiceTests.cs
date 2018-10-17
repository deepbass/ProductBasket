using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DecisionTechProductBasket;
using DecisionTechProductBasket.Services;
using DecisionTechProductBasket.Models;
using Moq;
using DecisionTechProductBasket.Repositories;
using DecisionTechProductBasket.Dtos;

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
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto { Id = "1", Products = new List<ProductDto>() });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked);

            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(0,result.TotalPriceInPounds);
            Assert.AreEqual(0, result.Products.Count);
        }

        [TestMethod]
        public void Given_OneBread_WhenGetCurrentBasket_ReturnBasketWithOneBreadWithEightyPenceTotalPrice()
        {
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto { Id = "1", Products = new List<ProductDto>
            {
                new ProductDto
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = Product.BreadId
                }
            }
            });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked);

            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(1.00m, result.TotalPriceInPounds);
            Assert.AreEqual(1, result.Products.Count);
            Assert.AreEqual(Product.BreadId, result.Products[0].Id);
        }

        [TestMethod]
        public void Given_OneBreadOneButterOneMilk_WhenGetCurrentBasket_ReturnBasketWithTwoPoundsNinetyFivePenceTotalPrice()
        {
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto
            {
                Id = "1",
                Products = new List<ProductDto>
            {
                 new ProductDto
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = "1"
                },
                new ProductDto
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 1,
                    Id = "2"
                },
                new ProductDto
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 1,
                    Id = "3"
                }
            }
            });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked);
           

            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(2.95m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_TwoBreadTwoButter_WhenGetCurrentBasket_ReturnBasketWithThreePoundsTenPenceTotalPrice()
        {
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto
            {
                Id = "1",
                Products = new List<ProductDto>
            {
                 new ProductDto
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 2,
                    Id = Product.BreadId
                },
                new ProductDto
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = Product.ButterId
                },
            }
            });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked); 

            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(3.10m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_FourMilk_WhenGetCurrentBasket_ReturnBasketWithThreePoundsFortyFivePenceTotalPrice()
        {
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto
            {
                Id = "1",
                Products = new List<ProductDto>
            {
                new ProductDto
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 4,
                    Id = Product.MilkId
                }
            }
            });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked); 

            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(3.45m, result.TotalPriceInPounds);
        }

        [TestMethod]
        public void Given_OneBreadTwoButterEightMilk_WhenGetCurrentBasket_ReturnBasketWithninePoundsTotalPrice()
        {
            Mock<IProductListRepository> mockProductRepository = new Mock<IProductListRepository>();
            mockProductRepository.Setup(m => m.GetProductListForId("1")).Returns(new ProductListDto
            {
                Id = "1",
                Products = new List<ProductDto>
            {
                new ProductDto
                {
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 1,
                    Id = Product.BreadId
                },
                new ProductDto
                {
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 2,
                    Id = Product.ButterId
                },
                new ProductDto
                {
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 8,
                    Id = Product.MilkId
                }
            }
            });
            IProductListRepository mocked = mockProductRepository.Object;
            BasketService service = new BasketService(_offerService, mocked);
            
            var result = service.GetCurrentBasket("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(9.00m, result.TotalPriceInPounds);
        }

    }
}
