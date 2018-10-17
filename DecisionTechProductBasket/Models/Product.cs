namespace DecisionTechProductBasket.Models
{
    public class Product
    {
        public static string BreadId = "bread";
        public static string ButterId = "butter";
        public static string MilkId = "milk";
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public decimal PriceInPounds { get; set; }

        public int Quantity { get; set; }
        
    }
}