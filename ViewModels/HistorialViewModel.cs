using ShopList.Models;
using ShopList.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace ShopList.ViewModels
{
    public class HistorialViewModel : BaseViewModel
    {
        public ObservableCollection<HistoryItem> Items { get; } = new();
        public ICommand LoadCommand { get; }

        public HistorialViewModel()
        {
            LoadCommand = new Command(Load);
            Load();
        }

        void Load()
        {
            Items.Clear();
            var ordered = DataService.Instance.History.OrderByDescending(h => h.CompletedAt);
            foreach(var h in ordered) Items.Add(h);
        }
    }
}
