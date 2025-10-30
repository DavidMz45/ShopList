using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class PlantillasPage : ContentPage
    {
        PlantillasViewModel vm;
        public PlantillasPage()
        {
            InitializeComponent();
            vm = new PlantillasViewModel();
            BindingContext = vm;
        }
    }
}
