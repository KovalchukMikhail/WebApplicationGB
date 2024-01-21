using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Seminar.Abstract;
using Seminar.Data;
using Seminar.Dto;
using Seminar.Models;

namespace Seminar.Services
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
