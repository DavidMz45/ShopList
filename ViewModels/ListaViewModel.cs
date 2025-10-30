using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ShopList.Models;
using ShopList.Services;
using Microsoft.Maui.Controls;

namespace ShopList.ViewModels
{
    public class ListaViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<Category> Categories { get; } = new();
        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ToggleBoughtCommand { get; }
        public ICommand CompleteListCommand { get; }

        public ListaViewModel()
        {
            AddCommand = new Command(async () => await Shell.Current.GoToAsync("productedit"));
            SaveCommand = new Command(async () => await SaveAsync());
            ToggleBoughtCommand = new Command<Product>(async (p) => await ToggleBought(p));
            CompleteListCommand = new Command(async () => await CompleteList());
            LoadFromService();
        }

        void LoadFromService()
        {
            Products.Clear();
            foreach(var p in DataService.Instance.Products)
                Products.Add(p);

            Categories.Clear();
            foreach(var c in DataService.Instance.Categories)
                Categories.Add(c);
        }

        public async Task SaveAsync()
        {
            DataService.Instance.Products = Products.ToList();
            await DataService.Instance.SaveAsync();
        }

        async Task ToggleBought(Product p)
        {
            if (p == null) return;
            p.IsBought = !p.IsBought;
            await SaveAsync();
        }

        async Task CompleteList()
        {
            var done = Products.Where(x => x.IsBought).ToList();
            if (!done.Any()) return;
            var history = new HistoryItem { CompletedAt = System.DateTime.UtcNow, Products = done.Select(d => new Product {
                Id = d.Id, Name = d.Name, Quantity = d.Quantity, Unit = d.Unit, CategoryId = d.CategoryId, IsBought = d.IsBought
            }).ToList()};

            DataService.Instance.History.Add(history);

            var remaining = Products.Where(x => !x.IsBought).ToList();
            Products.Clear();
            foreach (var r in remaining) Products.Add(r);

            await DataService.Instance.SaveAsync();
        }
    }
}
