using Seminar.Dto;

namespace Seminar.Abstract
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        int AddCategory(CategoryDto categoryDto);
    }
}
