using Microsoft.Maui.Controls;
using ShopList.Models;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class ListaPage : ContentPage
{
    private readonly ListaViewModel _viewModel;

    public ListaPage(ListaViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCommand.ExecuteAsync(null);
    }

    private async void OnProductCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Product product)
        {
            if (e.Value != e.PreviousValue)
            {
                await _viewModel.TogglePurchasedCommand.ExecuteAsync(product);
            }
        }
    }
}
