using System.Collections.Generic;
using System.Threading.Tasks;
using ShopList.Models;

namespace ShopList.Repositories;

public interface ICategoryRepository
{
    Task<IList<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<int> SaveAsync(Category category);
    Task DeleteAsync(int id);
}
