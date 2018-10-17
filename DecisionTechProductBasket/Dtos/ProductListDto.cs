using Newtonsoft.Json;
using System.Collections.Generic;

namespace DecisionTechProductBasket.Dtos
{
    public class ProductListDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
}