using Lab1.DAL;
using Lab1.Models;

namespace Lab1.BLL {
    public class ProductService {
        private readonly ProductDAL _productDal;

        public ProductService(ProductDAL productDal) {
            _productDal = productDal;
        }

        public List<Product> GetProducts() {
            return _productDal.GetProducts();
        }

        public Product GetProduct(int id) {
            return _productDal.GetProduct(id);
        }

        public void AddProduct(Product product) {
            _productDal.AddProduct(product);
        }

        public async Task UpdateProduct(Product product) {
            await _productDal.UpdateProduct(product);
        }

        public async Task DeleteProduct(int id) {
            _productDal.DeleteProduct(id);
        }
    }
}
