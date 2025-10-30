using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using SQLite;
using ShopList.Models;

namespace ShopList.Data;

public class Database
{
    private const string DatabaseFilename = "shoplist.db3";
    private readonly SQLiteAsyncConnection _connection;
    private bool _initialized;

    public Database()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        _connection = new SQLiteAsyncConnection(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
    }

    public SQLiteAsyncConnection Connection => _connection;

    public async Task InitializeAsync()
    {
        if (_initialized)
        {
            return;
        }

        await _connection.CreateTableAsync<Category>();
        await _connection.CreateTableAsync<Product>();
        await _connection.CreateTableAsync<TemplateList>();
        await _connection.CreateTableAsync<History>();

        await SeedCategoriesAsync();

        _initialized = true;
    }

    private async Task SeedCategoriesAsync()
    {
        var count = await _connection.Table<Category>().CountAsync();
        if (count > 0)
        {
            return;
        }

        await _connection.InsertAllAsync(new[]
        {
            new Category { Name = "Alimentos" },
            new Category { Name = "Limpieza" },
            new Category { Name = "Otros" }
        });
    }
}
