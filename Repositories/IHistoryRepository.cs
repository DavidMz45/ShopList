using System.Collections.Generic;
using System.Threading.Tasks;
using ShopList.Models;

namespace ShopList.Repositories;

public interface IHistoryRepository
{
    Task<IList<History>> GetAllAsync();
    Task<int> SaveAsync(History history);
    Task DeleteAsync(int id);
    Task ClearAsync();
}
