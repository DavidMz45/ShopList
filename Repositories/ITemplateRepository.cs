using System.Collections.Generic;
using System.Threading.Tasks;
using ShopList.Models;

namespace ShopList.Repositories;

public interface ITemplateRepository
{
    Task<IList<TemplateList>> GetAllAsync();
    Task<TemplateList?> GetByIdAsync(int id);
    Task<int> SaveAsync(TemplateList template);
    Task DeleteAsync(int id);
    Task<IList<TemplateList>> LoadFromFileAsync();
}
