using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class HistorialPage : ContentPage
    {
        HistorialViewModel vm;
        public HistorialPage()
        {
            InitializeComponent();
            vm = new HistorialViewModel();
            BindingContext = vm;
        }
    }
}
