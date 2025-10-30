using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class ProductoEditarCrearPage : ContentPage
    {
        ProductoEditarCrearViewModel vm;
        public ProductoEditarCrearPage()
        {
            InitializeComponent();
            vm = new ProductoEditarCrearViewModel();
            BindingContext = vm;
        }
    }
}
