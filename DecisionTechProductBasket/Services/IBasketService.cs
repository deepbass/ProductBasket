using DecisionTechProductBasket.Models;

namespace DecisionTechProductBasket.Services
{
    public interface IBasketService
    {
        Basket GetCurrentBasket();
    }
}