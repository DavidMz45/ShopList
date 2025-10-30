using System.Collections.Generic;
using Microsoft.Maui.Controls;
using ShopList.ViewModels;

namespace ShopList.Views;

public partial class ProductoEditarCrearPage : ContentPage, IQueryAttributable
{
    private readonly ProductoEditViewModel _viewModel;
    private int? _productId;
    private bool _isInitialized;

    public ProductoEditarCrearPage(ProductoEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_isInitialized)
        {
            await _viewModel.InitializeAsync(_productId);
            _isInitialized = true;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ProductId", out var value))
        {
            if (value is int id)
            {
                _productId = id;
            }
            else if (value is string str && int.TryParse(str, out var parsed))
            {
                _productId = parsed;
            }
        }
    }
}
