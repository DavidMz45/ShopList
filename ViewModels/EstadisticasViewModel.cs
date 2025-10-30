using System.Collections.Generic;
using System.Linq;
using ShopList.Services;
using ShopList.Models;

namespace ShopList.ViewModels
{
    public class EstadisticasViewModel : BaseViewModel
    {
        public Dictionary<string, int> CountByCategory { get; } = new();

        public EstadisticasViewModel()
        {
            Recalc();
        }

        public void Recalc()
        {
            CountByCategory.Clear();
            var cats = DataService.Instance.Categories.ToDictionary(c => c.Id, c => c.Name);
            foreach(var p in DataService.Instance.Products)
            {
                var key = p.CategoryId != null && cats.ContainsKey(p.CategoryId) ? cats[p.CategoryId] : "Sin categoría";
                if (!CountByCategory.ContainsKey(key)) CountByCategory[key] = 0;
                CountByCategory[key]++;
            }
        }
    }
}
