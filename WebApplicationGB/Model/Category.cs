﻿namespace WebApplicationGB.Model
{
    public class Category : BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
