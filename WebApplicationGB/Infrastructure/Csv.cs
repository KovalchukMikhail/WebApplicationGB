using Microsoft.Extensions.Caching.Memory;
using System.Text;
using WebApplicationGB.Dto;

namespace WebApplicationGB.Infrastructure
{
    public class Csv
    {
        public static string GetProductCsv(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();
            foreach(ProductDto product in products)
            {
                sb.Append(product.Id + ";" + product.Name + ";" + product.Description + ";" + product.CategoryID + "\n");
            }
            return sb.ToString();
        }
        public static string GetCategoryCsv(IEnumerable<CategoryDto> categories)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CategoryDto Category in categories)
            {
                sb.Append(Category.Id + ";" + Category.Name + ";" + Category.Description + ";" + "\n");
            }
            return sb.ToString();
        }
    }
}
