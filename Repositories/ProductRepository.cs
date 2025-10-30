using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Data;
using ShopList.Models;

namespace ShopList.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Database _database;

    public ProductRepository(Database database)
    {
        _database = database;
    }

    public async Task<IList<Product>> GetProductsAsync(int? categoryId = null, bool includePurchased = true, bool orderByCategory = false)
    {
        await _database.InitializeAsync();
        var query = _database.Connection.Table<Product>();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!includePurchased)
        {
            query = query.Where(p => !p.IsPurchased);
        }

        query = orderByCategory
            ? query.OrderBy(p => p.CategoryId).ThenBy(p => p.Name)
            : query.OrderBy(p => p.Name);

        return await query.ToListAsync();
    }

    public async Task<IList<Product>> GetPendingAsync(int? categoryId = null, bool orderByCategory = false)
        => await GetProductsAsync(categoryId, includePurchased: false, orderByCategory);

    public async Task<IList<Product>> GetPurchasedAsync(int? categoryId = null)
    {
        await _database.InitializeAsync();
        var query = _database.Connection.Table<Product>().Where(p => p.IsPurchased);
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        await _database.InitializeAsync();
        return await _database.Connection.FindAsync<Product>(id);
    }

    public async Task<int> SaveAsync(Product product)
    {
        await _database.InitializeAsync();
        product.Name = product.Name.Trim();
        product.UpdatedAt = DateTime.UtcNow;
        if (product.Id == 0)
        {
            product.CreatedAt = DateTime.UtcNow;
            return await _database.Connection.InsertAsync(product);
        }

        return await _database.Connection.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _database.InitializeAsync();
        await _database.Connection.DeleteAsync<Product>(id);
    }

    public async Task DeleteAsync(IEnumerable<int> ids)
    {
        await _database.InitializeAsync();
        foreach (var id in ids)
        {
            await _database.Connection.DeleteAsync<Product>(id);
        }
    }

    public async Task ClearAsync()
    {
        await _database.InitializeAsync();
        await _database.Connection.DeleteAllAsync<Product>();
    }
}
