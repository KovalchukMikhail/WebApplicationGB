using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StorageApi.Abstract;
using StorageApi.Data;
using StorageApi.Dto;
using StorageApi.Models;

namespace StorageApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        public ProductDto GetProduct(int productId)
        {
            using (_context)
            {
                Product product = _context.Products.Where(p => p.Id == productId).FirstOrDefault();
                ProductDto productDto = _mapper.Map<ProductDto>(product);
                return productDto;
            }
        }
    }
}
