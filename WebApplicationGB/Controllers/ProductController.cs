using Microsoft.AspNetCore.Mvc;
using WebApplicationGB.Model;

namespace WebApplicationGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("getProduct")]
        public IActionResult GetProduct()
        {
            try
            {
                using(ProductContext context = new ProductContext())
                {
                    var products = context.Products.Select(p => new Product()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description
                    }).ToList();
                    return Ok(products);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("postProduct")]
        public IActionResult PostProduct([FromQuery] string name, string description, int categoryId, int cost)
        {
            try
            {
                using (ProductContext context = new ProductContext())
                {
                    if(!context.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        context.Add(new Product
                        {
                            Name = name,
                            Description = description,
                            Cost = cost,
                            CategoryID = categoryId
                        });
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
        [HttpDelete("deleteProduct")]
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
    }
}
