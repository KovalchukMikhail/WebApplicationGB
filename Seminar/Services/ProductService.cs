using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Seminar.Abstract;
using Seminar.Data;
using Seminar.Dto;
using Seminar.Models;

namespace Seminar.Services
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
        public int AddProduct(ProductDto productDto)
        {
            using (_context)
            {
                var entity = _mapper.Map<Product>(productDto);
                _context.Products.Add(entity);
                _context.SaveChanges();
                _cache.Remove("products");
                return entity.Id;
            }

        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> products))
                return products;

            using (_context)
            {
                products = _context.Products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
