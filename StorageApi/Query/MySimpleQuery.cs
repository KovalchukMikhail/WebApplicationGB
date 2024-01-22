using StorageApi.Abstract;
using StorageApi.Dto;

namespace StorageApi.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService service) => service.GetStorages();
        public ProductDto GetProduct([Service] IProductService service, int productId) => service.GetProduct(productId);
    }
}
