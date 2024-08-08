using Lab1.BLL;
using Lab1.DAL;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace Lab1.Controllers {
    [Authorize]
    public class ProductController : Controller {

		private readonly ProductService _productService;
		public ProductController(ProductService productService) {
			_productService = productService;
		}
		public async Task<IActionResult> Index() {
			var products = _productService.GetProducts();
			return View(products);
		}

		[HttpGet]
		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(ProductViewModel product) {
			using (HttpClient client = new HttpClient()) {
				var random = new Random();
				int id = random.Next(1, 20);
				HttpResponseMessage response = await client.GetAsync($"https://fakestoreapi.com/products/{id}");
				response.EnsureSuccessStatusCode();

				var data = await response.Content.ReadAsStringAsync();
				var apiProduct = JsonSerializer.Deserialize<NewProduct>(data, new JsonSerializerOptions {
					PropertyNameCaseInsensitive = true
				});

				Console.WriteLine();
				Console.WriteLine(data);
				Console.WriteLine(apiProduct);

				Product newProduct = new Product {
					Title = apiProduct?.Title ?? "",
					Description = apiProduct?.Description ?? "",
					Price = apiProduct?.Price ?? 0,
					Category = apiProduct?.Category ?? "",
					Quantity = product?.Quantity ?? 0
				};

				_productService.AddProduct(newProduct);
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public IActionResult Update(int id) {
			var productToEdit = _productService.GetProduct(id);
			if (productToEdit != null) {
				return View(productToEdit);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Update(Product updatedProduct) {
			if (ModelState.IsValid) {
				await _productService.UpdateProduct(updatedProduct);
				return RedirectToAction("Index");
			}
			return View(updatedProduct);
		}

		public IActionResult Delete(int id) {
			var product = _productService.GetProduct(id);

			if (product != null) {
				return View(product);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> ConfirmDelete(int ProductId) {
			var product = _productService.GetProduct(ProductId);

			if (product != null) {
				await _productService.DeleteProduct(ProductId);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}
