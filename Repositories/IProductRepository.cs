using System.Collections.Generic;
using System.Threading.Tasks;
using ShopList.Models;

namespace ShopList.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetProductsAsync(int? categoryId = null, bool includePurchased = true, bool orderByCategory = false);
    Task<IList<Product>> GetPendingAsync(int? categoryId = null, bool orderByCategory = false);
    Task<IList<Product>> GetPurchasedAsync(int? categoryId = null);
    Task<Product?> GetByIdAsync(int id);
    Task<int> SaveAsync(Product product);
    Task DeleteAsync(int id);
    Task DeleteAsync(IEnumerable<int> ids);
    Task ClearAsync();
}
