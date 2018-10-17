using DecisionTechProductBasket.Models;

namespace DecisionTechProductBasket.Services
{
    public interface IBasketService
    {
        Basket GetCurrentBasket(string id);
        void UpdateBasket(Basket basket);
    }
}