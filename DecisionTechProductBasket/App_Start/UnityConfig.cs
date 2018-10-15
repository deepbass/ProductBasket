using DecisionTechProductBasket.Models;
using DecisionTechProductBasket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.RegistrationByConvention;

namespace DecisionTechProductBasket
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            var offerRepository = new OfferService();
            container.RegisterInstance<IOfferService>(offerRepository);
            container.RegisterInstance<IBasketService>(new BasketService(offerRepository,new List<Product>() {
                new Product
                {
                    Type = Product.ProductType.Bread,
                    PriceInPounds = 1.00m,
                    DisplayName = "Bread",
                    Quantity = 0,
                    Id = 1
                },
                new Product
                {
                    Type = Product.ProductType.Butter,
                    PriceInPounds = 0.80m,
                    DisplayName = "Butter",
                    Quantity = 0,
                    Id = 2
                },
                new Product
                {
                    Type = Product.ProductType.Milk,
                    PriceInPounds = 1.15m,
                    DisplayName = "Milk",
                    Quantity = 0,
                    Id = 3
                }
            }));
        }
    }
}