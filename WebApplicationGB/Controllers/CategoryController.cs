using Microsoft.AspNetCore.Mvc;
using WebApplicationGB.Dto;
using WebApplicationGB.Model;
using WebApplicationGB.Repo;

namespace WebApplicationGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public CategoryController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("get_categories")]
        public IActionResult GetCategories()
        {
            var categories = _productRepository.GetCategories();
            return Ok(categories);
        }

        [HttpPost("add_category")]
        public IActionResult PostCategory([FromBody] CategoryDto categoryDto)
        {
            var result = _productRepository.AddCategory(categoryDto);
            return Ok(result);
        }
        [HttpDelete("delete_category")]
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
