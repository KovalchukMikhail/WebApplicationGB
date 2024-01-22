using StorageApi.Dto;

namespace StorageApi.Abstract
{
    public interface IProductService
    {
        ProductDto GetProduct(int productId);
    }
}
