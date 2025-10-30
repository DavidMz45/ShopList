using ShopList.Models;
using ShopList.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ShopList.ViewModels
{
    public class ProductoEditarCrearViewModel : BaseViewModel
    {
        public Product Item { get; set; } = new();
        public ObservableCollection<Category> Categories { get; } = new();
        public Category SelectedCategory
        {
            get => Categories.Count==0?null:Categories[0];
            set
            {
                if (value!=null) Item.CategoryId = value.Id;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ProductoEditarCrearViewModel()
        {
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            foreach(var c in DataService.Instance.Categories) Categories.Add(c);
        }

        async System.Threading.Tasks.Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Item.Name)) return;
            var ds = DataService.Instance;
            var existing = ds.Products.Find(p => p.Id == Item.Id);
            if (existing == null)
            {
                ds.Products.Add(Item);
            }
            else
            {
                existing.Name = Item.Name;
                existing.Quantity = Item.Quantity;
                existing.Unit = Item.Unit;
                existing.CategoryId = Item.CategoryId;
                existing.IsBought = Item.IsBought;
            }
            await ds.SaveAsync();
            await Shell.Current.GoToAsync("//lista");
        }
    }
}
