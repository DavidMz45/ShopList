using ShopList.Models;
using ShopList.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ShopList.ViewModels
{
    public class CategoriasViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; } = new();
        public string NewName { get; set; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoriasViewModel()
        {
            AddCommand = new Command(async () => await AddAsync());
            DeleteCommand = new Command<Category>(async (c) => await DeleteAsync(c));
            Load();
        }

        void Load()
        {
            Categories.Clear();
            foreach(var c in DataService.Instance.Categories) Categories.Add(c);
        }

        async System.Threading.Tasks.Task AddAsync()
        {
            if (string.IsNullOrWhiteSpace(NewName)) return;
            var cat = new Category { Name = NewName.Trim() };
            DataService.Instance.Categories.Add(cat);
            NewName = "";
            Load();
            await DataService.Instance.SaveAsync();
        }

        async System.Threading.Tasks.Task DeleteAsync(Category c)
        {
            if (c == null) return;
            DataService.Instance.Categories.RemoveAll(x => x.Id == c.Id);
            foreach(var p in DataService.Instance.Products.Where(x=> x.CategoryId == c.Id))
                p.CategoryId = null;
            Load();
            await DataService.Instance.SaveAsync();
        }
    }
}
