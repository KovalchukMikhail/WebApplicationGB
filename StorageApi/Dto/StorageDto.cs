﻿using StorageApi.Models;

namespace StorageApi.Dto
{
    public class StorageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
