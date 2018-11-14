﻿namespace AspNetCore21MvcIdentity.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}