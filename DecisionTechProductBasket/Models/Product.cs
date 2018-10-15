namespace DecisionTechProductBasket.Models
{
    public class Product
    {
        public enum ProductType { Bread, Milk, Butter }

        public int Id { get; set; }
        public string DisplayName { get; set; }

        public ProductType Type { get; set; }

        public decimal PriceInPounds { get; set; }

        public int Quantity { get; set; }
        
    }
}