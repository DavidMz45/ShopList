using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Data;
using ShopList.Models;

namespace ShopList.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly Database _database;

    public CategoryRepository(Database database)
    {
        _database = database;
    }

    public async Task<IList<Category>> GetAllAsync()
    {
        await _database.InitializeAsync();
        var query = _database.Connection.Table<Category>().OrderBy(c => c.Name);
        return await query.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        await _database.InitializeAsync();
        return await _database.Connection.FindAsync<Category>(id);
    }

    public async Task<int> SaveAsync(Category category)
    {
        await _database.InitializeAsync();
        category.Name = category.Name.Trim();

        var existing = await _database.Connection.Table<Category>()
            .Where(c => c.Name == category.Name && c.Id != category.Id)
            .FirstOrDefaultAsync();
        if (existing != null)
        {
            throw new InvalidOperationException("La categoría ya existe.");
        }

        if (category.Id == 0)
        {
            return await _database.Connection.InsertAsync(category);
        }

        return await _database.Connection.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        await _database.InitializeAsync();
        var count = await _database.Connection.Table<Product>().Where(p => p.CategoryId == id).CountAsync();
        if (count > 0)
        {
            throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos asociados.");
        }

        await _database.Connection.DeleteAsync<Category>(id);
    }
}
