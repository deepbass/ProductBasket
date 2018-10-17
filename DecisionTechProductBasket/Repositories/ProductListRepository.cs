using DecisionTechProductBasket.Dtos;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DecisionTechProductBasket.Repositories
{
    public class ProductListRepository : IProductListRepository
    {
        private static FeedOptions _queryOptions = new FeedOptions { MaxItemCount = -1 };
        private static DocumentClient _client;
        private static Uri _documentCollection;
        private static bool usingCosmosDB = false;
        private static ProductListDto _productListDto;
        public ProductListRepository()
        {
            // Will try and use CosmosDB, if not setup will default to hardcoded list.
            try
            {
                _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["CosmosDBEndpoint"]), ConfigurationManager.AppSettings["PrimaryKey"]);
                _documentCollection = UriFactory.CreateDocumentCollectionUri(ConfigurationManager.AppSettings["DatabaseName"], ConfigurationManager.AppSettings["CollectionName"]);
                _client.CreateDocumentQuery<ProductListDto>(_documentCollection, _queryOptions);
                usingCosmosDB = true;
            }
            catch (Exception e)
            {
                _productListDto = new ProductListDto
                {
                    Id = "1",
                    Products = new List<ProductDto>
                    {
                        new ProductDto
                        {
                            PriceInPounds = 1.00m,
                            DisplayName = "Bread",
                            Quantity = 0,
                            Id = "1"
                        },
                        new ProductDto
                        {
                            PriceInPounds = 0.80m,
                            DisplayName = "Butter",
                            Quantity = 0,
                            Id = "2"
                        },
                        new ProductDto
                        {
                            PriceInPounds = 1.15m,
                            DisplayName = "Milk",
                            Quantity = 0,
                            Id = "3"
                        }
                }
                };
            }
        }

        public ProductListDto GetProductListForId(string id)
        {
            if (usingCosmosDB)
            {

                IQueryable<ProductListDto> productQuery = _client.CreateDocumentQuery<ProductListDto>(_documentCollection, _queryOptions)
                    .Where(f => f.Id == id);
                var results = productQuery.ToList();
                return results.FirstOrDefault();
            } else
            {
                return _productListDto;
            }
        }

        public void UpdateProductList(ProductListDto productList)
        {
            if (usingCosmosDB)
            {
                var result = _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(ConfigurationManager.AppSettings["DatabaseName"], ConfigurationManager.AppSettings["CollectionName"], productList.Id), productList).Result;
            } else
            {
                _productListDto.Products.Clear();
                foreach(var product in productList.Products)
                {
                    _productListDto.Products.Add(product);
                }
            }
        }
    }
}