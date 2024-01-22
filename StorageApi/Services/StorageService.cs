using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StorageApi.Abstract;
using StorageApi.Data;
using StorageApi.Dto;
using StorageApi.Models;

namespace StorageApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public StorageService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddStorage(StorageDto storageDto)
        {
            using (_context)
            {
                var entity = _mapper.Map<Storage>(storageDto);
                _context.Storages.Add(entity);
                _context.SaveChanges();
                _cache.Remove("storages");
                return entity.Id;
            }
        }
        public StorageDto AddProductToStorage(int storageId, int productId, int count)
        {
            using (_context)
            {
                Product product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
                Storage storage = _context.Storages.Where(s => s.Id == storageId).FirstOrDefault();
                storage.Products.Add(product);
                storage.Count = count;
                _context.SaveChanges();
                storage = _context.Storages.Where(s => s.Id == storageId).FirstOrDefault();
                StorageDto result = _mapper.Map<StorageDto>(storage);
                return result;
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            if (_cache.TryGetValue("storages", out List<StorageDto> storages))
                return storages;

            using (_context)
            {
                storages = _context.Storages.Select(p => _mapper.Map<StorageDto>(p)).ToList();
                _cache.Set("storages", storages, TimeSpan.FromMinutes(30));
                return storages;
            }
        }
    }
}
