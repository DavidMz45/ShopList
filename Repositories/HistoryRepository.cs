using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopList.Data;
using ShopList.Models;

namespace ShopList.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly Database _database;

    public HistoryRepository(Database database)
    {
        _database = database;
    }

    public async Task<IList<History>> GetAllAsync()
    {
        await _database.InitializeAsync();
        var query = _database.Connection.Table<History>().OrderByDescending(h => h.Date);
        return await query.ToListAsync();
    }

    public async Task<int> SaveAsync(History history)
    {
        await _database.InitializeAsync();
        history.ItemsJson = history.ItemsJson ?? "[]";
        if (history.Id == 0)
        {
            return await _database.Connection.InsertAsync(history);
        }

        return await _database.Connection.UpdateAsync(history);
    }

    public async Task DeleteAsync(int id)
    {
        await _database.InitializeAsync();
        await _database.Connection.DeleteAsync<History>(id);
    }

    public async Task ClearAsync()
    {
        await _database.InitializeAsync();
        await _database.Connection.DeleteAllAsync<History>();
    }
}
