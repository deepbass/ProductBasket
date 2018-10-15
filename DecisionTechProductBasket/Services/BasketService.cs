using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DecisionTechProductBasket.Models;

namespace DecisionTechProductBasket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOfferService _offerService;
        private IList<Product> _products;

        public BasketService( IOfferService offerService, IList<Product> startingProducts)
        {
            this._offerService = offerService;
            this._products = startingProducts;
        }
        public Basket GetCurrentBasket()
        {
            return new Basket { TotalPriceInPounds = CalculateValueOfProducts(_products), Products = _products, Id = 1 };
        }

        private decimal CalculateValueOfProducts(IList<Product> products)
        {
            var totalBeforeOffers = products.Sum(product => product.Quantity * product.PriceInPounds);
            var totalAfterOffers = totalBeforeOffers - _offerService.ApplyOffers(products);
            return totalAfterOffers;
        }
    }
}