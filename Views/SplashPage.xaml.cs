using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class SplashPage : ContentPage
{
    private readonly SplashViewModel _viewModel;

    public SplashPage(SplashViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeCommand.ExecuteAsync(null);
    }
}
