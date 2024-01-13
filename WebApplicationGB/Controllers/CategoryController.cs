using Microsoft.AspNetCore.Mvc;
using WebApplicationGB.Model;

namespace WebApplicationGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("getCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                using (ProductContext context = new ProductContext())
                {
                    var categorys = context.Products.Select(p => new Category()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description
                    }).ToList();
                    return Ok(categorys);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("postCategory")]
        public IActionResult PostCategory([FromQuery] string name, string description)
        {
            try
            {
                using (ProductContext context = new ProductContext())
                {
                    if (!context.Categories.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        context.Add(new Category
                        {
                            Name = name,
                            Description = description,
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
        [HttpDelete("deleteCategory")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                using (ProductContext context = new ProductContext())
                {
                    Category category = context.Categories.FirstOrDefault(x => x.Id == categoryId);
                    if (category != null)
                    {
                        context.Remove(category);
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
