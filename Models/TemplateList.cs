using System;
using System.Collections.Generic;

namespace ShopList.Models
{
    public class TemplateList
    {
        public string Id { get; set;} = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
