using System;
using System.Collections.Generic;
using System.Text.Json;
using SQLite;

namespace ShopList.Models;

public class History
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public string ItemsJson { get; set; } = "[]";

    [Ignore]
    public IList<Product> Items
    {
        get => string.IsNullOrWhiteSpace(ItemsJson)
            ? new List<Product>()
            : JsonSerializer.Deserialize<List<Product>>(ItemsJson) ?? new List<Product>();
        set => ItemsJson = JsonSerializer.Serialize(value ?? new List<Product>());
    }
}
