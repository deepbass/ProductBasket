using DecisionTechProductBasket.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTechProductBasket.Repositories
{
    public interface IProductListRepository
    {
        ProductListDto GetProductListForId(string id);

        void UpdateProductList(ProductListDto productList);
    }
}
