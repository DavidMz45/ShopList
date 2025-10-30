using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ShopList.Models;
using ShopList.Repositories;

namespace ShopList.ViewModels;

public partial class EstadisticasViewModel : BaseViewModel
{
    private readonly IHistoryRepository _historyRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ObservableCollection<CategoryStat> Statistics { get; } = new();

    public EstadisticasViewModel(IHistoryRepository historyRepository,
                                 IProductRepository productRepository,
                                 ICategoryRepository categoryRepository)
    {
        _historyRepository = historyRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        Title = "Estadísticas";
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            SetBusy(true);
            var categories = await _categoryRepository.GetAllAsync();
            var nameMap = categories.ToDictionary(c => c.Id, c => c.Name);
            var totals = new Dictionary<int, int>();

            var histories = await _historyRepository.GetAllAsync();
            foreach (var history in histories)
            {
                foreach (var item in history.Items)
                {
                    if (totals.ContainsKey(item.CategoryId))
                    {
                        totals[item.CategoryId] += 1;
                    }
                    else
                    {
                        totals[item.CategoryId] = 1;
                    }
                }
            }

            var purchased = await _productRepository.GetPurchasedAsync();
            foreach (var item in purchased)
            {
                if (totals.ContainsKey(item.CategoryId))
                {
                    totals[item.CategoryId] += 1;
                }
                else
                {
                    totals[item.CategoryId] = 1;
                }
            }

            Statistics.Clear();
            foreach (var total in totals.OrderByDescending(t => t.Value))
            {
                var name = nameMap.TryGetValue(total.Key, out var value) ? value : "Sin categoría";
                Statistics.Add(new CategoryStat(name, total.Value));
            }
        }
        finally
        {
            SetBusy(false);
        }
    }
}

public record CategoryStat(string Category, int Count);
