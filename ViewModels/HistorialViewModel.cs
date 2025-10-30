using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ShopList.Models;
using ShopList.Repositories;

namespace ShopList.ViewModels;

public partial class HistorialViewModel : BaseViewModel
{
    private readonly IHistoryRepository _historyRepository;

    public ObservableCollection<History> HistoryItems { get; } = new();

    public HistorialViewModel(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
        Title = "Historial";
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
            var histories = await _historyRepository.GetAllAsync();
            HistoryItems.Clear();
            foreach (var history in histories)
            {
                HistoryItems.Add(history);
            }
        }
        finally
        {
            SetBusy(false);
        }
    }
}
