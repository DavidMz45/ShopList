using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class CategoriasPage : ContentPage
    {
        CategoriasViewModel vm;
        public CategoriasPage()
        {
            InitializeComponent();
            vm = new CategoriasViewModel();
            BindingContext = vm;
        }
    }
}
