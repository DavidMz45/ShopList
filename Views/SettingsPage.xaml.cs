using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel vm;
        public SettingsPage()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            BindingContext = vm;
        }
    }
}
