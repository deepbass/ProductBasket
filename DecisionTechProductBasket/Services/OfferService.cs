using DecisionTechProductBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionTechProductBasket.Services
{
    public class OfferService : IOfferService
    {
        private IList<Func<IList<Product>, decimal>> _offers;

        public OfferService()
        {
            _offers = new List<Func<IList<Product>, decimal>>()
                        {
                            BuyTwoButterGetOneBreadHalfPrice,
                            BuyThreeMilksGetOneFree
                        };
        }

        public decimal ApplyOffers(IList<Product> products)
        {
            var currentDiscount = 0m;
            foreach (var offer in _offers)
            {
                currentDiscount += offer(products);
            }
            return currentDiscount;
        }

        private decimal BuyTwoButterGetOneBreadHalfPrice(IList<Product> products)
        {
            
            var discount = 0m;
            var bread = products.FirstOrDefault(product => product.Type == Product.ProductType.Bread);
            var numberOfBread = bread?.Quantity ?? 0;
            var numberOfButter = products.FirstOrDefault(product => product.Type == Product.ProductType.Butter)?.Quantity ?? 0;
            for (var i = 0; i < numberOfButter; i++)
            {
                var currentButter = i + 1;
                // if theres twice as many butters as bread, then a discount has occurred
                if (currentButter % 2 == 0 && currentButter / 2 >= numberOfBread / currentButter)
                {
                    discount += bread?.PriceInPounds / 2 ?? 0;
                }
            }
            return discount;
        }
        private decimal BuyThreeMilksGetOneFree(IList<Product> products)
        {
            var discount = 0m;
            var milk = products.FirstOrDefault(product => product.Type == Product.ProductType.Milk);
            int numberOfFourPacksOfMilk = milk?.Quantity / 4 ?? 0;
            discount = numberOfFourPacksOfMilk * milk?.PriceInPounds ?? 0;
            return discount;
        }
    }
}