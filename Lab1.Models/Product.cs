
namespace Lab1.Models {
    public class Product {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}
 