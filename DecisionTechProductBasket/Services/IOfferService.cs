using DecisionTechProductBasket.Models;
using System;
using System.Collections.Generic;

namespace DecisionTechProductBasket.Services
{
    public interface IOfferService
    {
        decimal ApplyOffers(IList<Product> products);
    }
}