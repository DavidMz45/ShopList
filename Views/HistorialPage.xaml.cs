using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class HistorialPage : ContentPage
{
    private readonly HistorialViewModel _viewModel;

    public HistorialPage(HistorialViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCommand.ExecuteAsync(null);
    }
}
