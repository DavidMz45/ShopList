using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views
{
    public partial class SplashPage : ContentPage
    {
        SplashViewModel vm;
        public SplashPage()
        {
            InitializeComponent();
            vm = new SplashViewModel();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadCommand.Execute(null);
        }
    }
}
