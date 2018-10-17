namespace DecisionTechProductBasket.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public decimal PriceInPounds { get; set; }

        public int Quantity { get; set; }
    }
}