using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionTechProductBasket.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public decimal TotalPriceInPounds { get; set; }
        public IList<Product> Products { get; set; }
    }
}