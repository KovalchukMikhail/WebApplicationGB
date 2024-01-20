using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using WebApplicationGB.Dto;
using WebApplicationGB.Model;

namespace WebApplicationGB.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private ProductContext _context;
        public ProductRepository(IMapper mapper, IMemoryCache cache, ProductContext context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }
        public int AddCategory(CategoryDto categoryDto)
        {
            using(_context)
            {
                var entityCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name.ToLower());
                if(entityCategory != null)
                {
                    return entityCategory.Id;
                }
                entityCategory = _mapper.Map<Category>(categoryDto);
                _context.Categories.Add(entityCategory);
                _context.SaveChanges();
                _cache.Remove("categories");
                return entityCategory.Id;
            }
        }

        public int AddProduct(ProductDto productDto)
        {
            using (_context)
            {
                var entityProduct = _context.Products.FirstOrDefault(p => p.Name.ToLower() == productDto.Name.ToLower());
                if (entityProduct != null)
                {
                    return entityProduct.Id;
                }
                entityProduct = _mapper.Map<Product>(productDto);
                _context.Products.Add(entityProduct);
                _context.SaveChanges();
                _cache.Remove("products");
                return entityProduct.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            if(_cache.TryGetValue("groups", out List<CategoryDto> categories))
            {
                return categories;
            }
            using(_context)
            {
                var categoryList = _context.Categories.Select(c => _mapper.Map<CategoryDto>(c)).ToList();
                _cache.Set("categories", categoryList, TimeSpan.FromMinutes(30));
                return categoryList;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> products))
            {   
                return products;
            }
            using (_context)
            {
                var productList = _context.Products.Select(c => _mapper.Map<ProductDto>(c)).ToList();
                _cache.Set("products", productList, TimeSpan.FromMinutes(30));
                return productList;
            }
        }
    }
}
