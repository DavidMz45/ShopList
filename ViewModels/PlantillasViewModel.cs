using ShopList.Models;
using ShopList.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ShopList.ViewModels
{
    public class PlantillasViewModel : BaseViewModel
    {
        public ObservableCollection<TemplateList> Templates { get; } = new();
        public string NewName { get; set; }
        public ICommand SaveCurrentAsTemplateCommand { get; }
        public ICommand ApplyTemplateCommand { get; }

        public PlantillasViewModel()
        {
            SaveCurrentAsTemplateCommand = new Command(async () => await SaveAsTemplate());
            ApplyTemplateCommand = new Command<TemplateList>(async (t) => await ApplyTemplate(t));
            Load();
        }

        void Load()
        {
            Templates.Clear();
            foreach(var t in DataService.Instance.Templates) Templates.Add(t);
        }

        async System.Threading.Tasks.Task SaveAsTemplate()
        {
            if (string.IsNullOrWhiteSpace(NewName)) return;
            var tpl = new TemplateList { Name = NewName.Trim() };
            tpl.Products = DataService.Instance.Products.Select(p => new Product {
                Name = p.Name, Quantity = p.Quantity, Unit = p.Unit, CategoryId = p.CategoryId
            }).ToList();
            DataService.Instance.Templates.Add(tpl);
            NewName = "";
            Load();
            await DataService.Instance.SaveAsync();
        }

        async System.Threading.Tasks.Task ApplyTemplate(TemplateList tpl)
        {
            if (tpl == null) return;
            DataService.Instance.Products = tpl.Products.Select(p => new Product {
                Name = p.Name, Quantity = p.Quantity, Unit = p.Unit, CategoryId = p.CategoryId
            }).ToList();
            await DataService.Instance.SaveAsync();
            await Shell.Current.GoToAsync("//lista");
        }
    }
}
