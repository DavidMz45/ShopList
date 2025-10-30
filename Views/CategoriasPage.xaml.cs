using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class CategoriasPage : ContentPage
{
    private readonly CategoriasViewModel _viewModel;

    public CategoriasPage(CategoriasViewModel viewModel)
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
