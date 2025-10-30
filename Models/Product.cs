using System;

namespace ShopList.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double Quantity { get; set; } = 1;
        public string Unit { get; set; } = string.Empty;
        public string CategoryId { get; set; }
        public bool IsBought { get; set; } = false;
    }
}
