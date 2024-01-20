using Microsoft.AspNetCore.Mvc;
using WebApplicationGB.Dto;
using WebApplicationGB.Infrastructure;
using WebApplicationGB.Model;
using WebApplicationGB.Repo;

namespace WebApplicationGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            var result = _productRepository.AddProduct(productDto);
            return Ok(result);
        }
        [HttpDelete("delete_product")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                using (ProductContext context = new ProductContext())
                {
                    Product product = context.Products.FirstOrDefault(x => x.Id == productId);
                    if (product != null)
                    {
                        context.Remove(product);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet(template: "get_product_csv")]
        public FileContentResult GetProductsCsv()
        {
            var products = _productRepository.GetProducts();
            string content = Csv.GetProductCsv(products);
            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }
    }
}
