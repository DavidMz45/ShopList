using System;
using System.Collections.Generic;

namespace ShopList.Models
{
    public class HistoryItem
    {
        public string Id { get; set;} = Guid.NewGuid().ToString();
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
        public List<Product> Products { get; set; } = new();
    }
}
