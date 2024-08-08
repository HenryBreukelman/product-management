using Lab1.Models;
using Microsoft.Extensions.Logging;

namespace Lab1.DAL {
    public class ProductDAL {
        private readonly Lab1Context _context;

        public ProductDAL(Lab1Context context) {
            _context = context;
        }

        public List<Product> GetProducts() {
            return _context.Products.ToList();
        }
        public Product GetProduct(int id) {
            return _context.Products.Find(id);
		}

		public void AddProduct(Product product) {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task UpdateProduct(Product product) {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public async Task DeleteProduct(int id) {
            var product = _context.Products.Find(id);
            if (product != null) {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
