using Seminar.Dto;

namespace Seminar.Abstract
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        int AddStorage(StorageDto storageDto);
    }
}
