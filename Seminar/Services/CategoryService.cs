using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Seminar.Abstract;
using Seminar.Data;
using Seminar.Dto;
using Seminar.Models;

namespace Seminar.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public CategoryService(AppDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddCategory(CategoryDto categoryDto)
        {
            using (_context)
            {
                var entity = _mapper.Map<Category>(categoryDto);
                _context.Categories.Add(entity);
                _context.SaveChanges();
                _cache.Remove("categories");
                return entity.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDto> categories))
                return categories;

            using (_context)
            {
                categories = _context.Categories.Select(p => _mapper.Map<CategoryDto>(p)).ToList();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(30));
                return categories;
            }
        }
    }
}
