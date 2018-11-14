using AspNetCore21MvcIdentity.Data.Entities;
using System.Collections.Generic;

namespace AspNetCore21MvcIdentity.ViewModels
{
    public class ProductViewModel
    {
        public string Title { get; set; }

        public List<Product> Products { get; set; }
    }
}
