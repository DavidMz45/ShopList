using SQLite;

namespace ShopList.Models;

public class Category
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;
}
