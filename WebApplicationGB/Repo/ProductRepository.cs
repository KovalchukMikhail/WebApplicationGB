using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApplicationGB.Dto;
using WebApplicationGB.Model;

namespace WebApplicationGB.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public ProductRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
        }
        public int AddCategory(CategoryDto categoryDto)
        {
            using(var context = new ProductContext())
            {
                var entityCategory = context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name.ToLower());
                if(entityCategory != null)
                {
                    return entityCategory.Id;
                }
                entityCategory = _mapper.Map<Category>(categoryDto);
                context.Categories.Add(entityCategory);
                context.SaveChanges();
                _cache.Remove("categories");
                return entityCategory.Id;
            }
        }

        public int AddProduct(ProductDto productDto)
        {
            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(p => p.Name.ToLower() == productDto.Name.ToLower());
                if (entityProduct != null)
                {
                    return entityProduct.Id;
                }
                entityProduct = _mapper.Map<Product>(productDto);
                context.Products.Add(entityProduct);
                context.SaveChanges();
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
            using( var context = new ProductContext())
            {
                var categoryList = context.Categories.Select(c => _mapper.Map<CategoryDto>(c)).ToList();
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
            using (var context = new ProductContext())
            {
                var productList = context.Products.Select(c => _mapper.Map<ProductDto>(c)).ToList();
                _cache.Set("products", productList, TimeSpan.FromMinutes(30));
                return productList;
            }
        }
    }
}
