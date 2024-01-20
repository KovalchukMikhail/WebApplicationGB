﻿using WebApplicationGB.Dto;
using WebApplicationGB.Model;

namespace WebApplicationGB.Repo
{
    public interface IProductRepository
    {
        public int AddCategory(CategoryDto category);
        public IEnumerable<CategoryDto> GetCategories();
        public int AddProduct(ProductDto product);
        public IEnumerable<ProductDto> GetProducts();
    }
}
