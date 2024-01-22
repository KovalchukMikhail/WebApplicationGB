using StorageApi.Dto;

namespace StorageApi.Abstract
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        StorageDto AddProductToStorage(int storageId, int productId, int count);
        int AddStorage(StorageDto storageDto);
    }
}
