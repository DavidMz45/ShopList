using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class EstadisticasPage : ContentPage
    {
        EstadisticasViewModel vm;
        public EstadisticasPage()
        {
            InitializeComponent();
            vm = new EstadisticasViewModel();
            BindingContext = vm;
        }
    }
}
