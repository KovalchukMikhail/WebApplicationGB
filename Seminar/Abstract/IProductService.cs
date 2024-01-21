using Seminar.Dto;

namespace Seminar.Abstract
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto productDto);
    }
}
