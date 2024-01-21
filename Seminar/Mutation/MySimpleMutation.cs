using Seminar.Abstract;
using Seminar.Dto;

namespace Seminar.Mutation
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService service, ProductDto product)
        {
            int id = service.AddProduct(product);
            return id;
        }
        public int AddCategory([Service] ICategoryService service, CategoryDto category)
        {
            int id = service.AddCategory(category);
            return id;
        }
    }
}
