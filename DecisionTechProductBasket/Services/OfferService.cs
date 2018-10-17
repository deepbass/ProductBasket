using DecisionTechProductBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Parallel.ForEach(_offers, (offer) =>
            {
                currentDiscount += offer(products);
            }); 
            // Will enable better performance if lots of offers, becomes impossible if order is introduced to reqs. 
            // Also I would move these offers out into Azure Functions, And this will reduce latency-induced wait time.
            return currentDiscount;
        }

        public decimal BuyTwoButterGetOneBreadHalfPrice(IList<Product> products) // These would be private, implemented using Azure FUnctions
        {
            
            var discount = 0m;
            var bread = products.FirstOrDefault(product => product.Id == Product.BreadId);
            var numberOfBread = bread?.Quantity ?? 0;
            var numberOfButter = products.FirstOrDefault(product => product.Id == Product.ButterId)?.Quantity ?? 0;
            var numberOfDiscounts = 0;
            for (var i = 0; i < numberOfButter; i++)
            {
                var currentButter = i + 1;
                // multiple of two of butter
                if (currentButter % 2 == 0) 
                {
                    // if theres some bread left
                    if(numberOfBread > 0)
                    {
                        numberOfBread--;
                        discount += bread?.PriceInPounds / 2 ?? 0;
                    } else
                    {
                        break;
                    }
                }
            }
            return discount;
        }
        public decimal BuyThreeMilksGetOneFree(IList<Product> products)
        {
            var discount = 0m;
            var milk = products.FirstOrDefault(product => product.Id == Product.MilkId);
            int numberOfFourPacksOfMilk = milk?.Quantity / 4 ?? 0;
            discount = numberOfFourPacksOfMilk * milk?.PriceInPounds ?? 0;
            return discount;
        }
    }
}