using System;
using SQLite;

namespace ShopList.Models;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int CategoryId { get; set; }

    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; } = 1;

    public bool IsPurchased { get; set; }

    [Ignore]
    public string CategoryName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
