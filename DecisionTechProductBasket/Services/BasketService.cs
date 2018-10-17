using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DecisionTechProductBasket.Dtos;
using DecisionTechProductBasket.Models;
using DecisionTechProductBasket.Repositories;

namespace DecisionTechProductBasket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOfferService _offerService;
        private readonly IProductListRepository _productRepository;

        public BasketService( IOfferService offerService, IProductListRepository productRepository)
        {
            _offerService = offerService;
            _productRepository = productRepository;
        }
        public Basket GetCurrentBasket(string id)
        {
            return MapProductListDtoToBasket(_productRepository.GetProductListForId(id));
        }

        public void UpdateBasket(Basket basket)
        {
            _productRepository.UpdateProductList(MapBasketToProductListDto(basket));
        }

        private decimal CalculateValueOfProducts(IList<Product> products)
        {
            var totalBeforeOffers = products.Sum(product => product.Quantity * product.PriceInPounds);
            var totalAfterOffers = totalBeforeOffers - _offerService.ApplyOffers(products);
            return totalAfterOffers;
        }

        private Basket MapProductListDtoToBasket(ProductListDto productListDto)
        {
            var productList = new List<Product>();
            foreach(var productDto in productListDto.Products)
            {
                productList.Add(MapProductDtoToProduct(productDto));
            }
            return new Basket
            {
                TotalPriceInPounds = CalculateValueOfProducts(productList),
                Products = productList,
                Id = productListDto.Id
            };
        }

        private Product MapProductDtoToProduct(ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                DisplayName = productDto.DisplayName,
                PriceInPounds = productDto.PriceInPounds,
                Quantity = productDto.Quantity,
            };
        }

        private ProductListDto MapBasketToProductListDto(Basket basket)
        {
            var productList = new List<ProductDto>();
            foreach (var product in basket.Products)
            {
                productList.Add(MapProductToProductDto(product));
            }
            return new ProductListDto { 
                Products = productList,
                Id = basket.Id
            };
        }

        private ProductDto MapProductToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                DisplayName = product.DisplayName,
                PriceInPounds = product.PriceInPounds,
                Quantity = product.Quantity,
            };
        }

        
    }
}