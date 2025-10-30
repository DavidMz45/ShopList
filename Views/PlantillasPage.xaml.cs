using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class PlantillasPage : ContentPage
{
    private readonly PlantillasViewModel _viewModel;

    public PlantillasPage(PlantillasViewModel viewModel)
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
