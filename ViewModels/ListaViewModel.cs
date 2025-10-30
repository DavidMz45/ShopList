using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using ShopList.Models;
using ShopList.Repositories;
using ShopList.Services;

namespace ShopList.ViewModels;

public partial class ListaViewModel : BaseViewModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly ISettingsService _settingsService;

    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();
    public ObservableCollection<Category> FilterCategories { get; } = new();

    [ObservableProperty]
    private Category? selectedFilter;

    public ListaViewModel(IProductRepository productRepository,
                          ICategoryRepository categoryRepository,
                          IHistoryRepository historyRepository,
                          ISettingsService settingsService)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _historyRepository = historyRepository;
        _settingsService = settingsService;
        Title = "Lista de compras";
    }

    partial void OnSelectedFilterChanged(Category? value)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await RefreshProductsAsync();
        });
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            SetBusy(true);
            await LoadCategoriesAsync();
            await RefreshProductsAsync();
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task LoadCategoriesAsync()
    {
        var previousFilterId = SelectedFilter?.Id;
        var categories = await _categoryRepository.GetAllAsync();
        Categories.Clear();
        foreach (var category in categories)
        {
            Categories.Add(category);
        }

        FilterCategories.Clear();
        FilterCategories.Add(new Category { Id = 0, Name = "Todas" });
        foreach (var category in categories)
        {
            FilterCategories.Add(category);
        }

        if (previousFilterId.HasValue)
        {
            SelectedFilter = FilterCategories.FirstOrDefault(c => c.Id == previousFilterId.Value) ?? FilterCategories.FirstOrDefault();
        }
        else
        {
            SelectedFilter = FilterCategories.FirstOrDefault();
        }
    }

    private async Task RefreshProductsAsync()
    {
        if (SelectedFilter == null)
        {
            return;
        }

        var order = _settingsService.GetListOrder();
        bool orderByCategory = order == ListOrderOption.ByCategory;
        int? categoryId = SelectedFilter.Id > 0 ? SelectedFilter.Id : null;
        var items = await _productRepository.GetProductsAsync(categoryId, includePurchased: true, orderByCategory: orderByCategory);

        var categoryNames = Categories.ToDictionary(c => c.Id, c => c.Name);

        Products.Clear();
        foreach (var item in items)
        {
            item.CategoryName = categoryNames.TryGetValue(item.CategoryId, out var name) ? name : "Sin categoría";
            Products.Add(item);
        }
    }

    [RelayCommand]
    private async Task TogglePurchasedAsync(Product product)
    {
        try
        {
            product.IsPurchased = !product.IsPurchased;
            await _productRepository.SaveAsync(product);
            await RefreshProductsAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private async Task AddProductAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.ProductoEditarCrearPage));
    }

    [RelayCommand]
    private async Task EditProductAsync(Product product)
    {
        await Shell.Current.GoToAsync(nameof(Views.ProductoEditarCrearPage), new Dictionary<string, object>
        {
            ["ProductId"] = product.Id
        });
    }

    [RelayCommand]
    private async Task DeleteProductAsync(Product product)
    {
        try
        {
            if (_settingsService.GetConfirmDeletion())
            {
                var confirm = await Shell.Current.DisplayAlert("Confirmación", $"¿Eliminar {product.Name}?", "Sí", "No");
                if (!confirm)
                {
                    return;
                }
            }

            await _productRepository.DeleteAsync(product.Id);
            await RefreshProductsAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private async Task CompleteListAsync()
    {
        var purchased = await _productRepository.GetPurchasedAsync();
        if (!purchased.Any())
        {
            await Shell.Current.DisplayAlert("Información", "No hay productos comprados para guardar en el historial.", "Aceptar");
            return;
        }

        var history = new History
        {
            Date = DateTime.Now,
            Items = purchased.ToList()
        };

        await _historyRepository.SaveAsync(history);
        await _productRepository.DeleteAsync(purchased.Select(p => p.Id));
        await RefreshProductsAsync();
        await Shell.Current.DisplayAlert("Éxito", "Lista guardada en historial.", "Aceptar");
    }

    [RelayCommand]
    private async Task ClearListAsync()
    {
        if (_settingsService.GetConfirmClear())
        {
            var confirm = await Shell.Current.DisplayAlert("Confirmación", "¿Desea limpiar la lista completa?", "Sí", "No");
            if (!confirm)
            {
                return;
            }
        }

        await _productRepository.ClearAsync();
        await RefreshProductsAsync();
    }

    [RelayCommand]
    private static Task NavigateToCategoriesAsync()
        => Shell.Current.GoToAsync(nameof(Views.CategoriasPage));

    [RelayCommand]
    private static Task NavigateToPlantillasAsync()
        => Shell.Current.GoToAsync(nameof(Views.PlantillasPage));

    [RelayCommand]
    private static Task NavigateToHistorialAsync()
        => Shell.Current.GoToAsync(nameof(Views.HistorialPage));

    [RelayCommand]
    private static Task NavigateToSettingsAsync()
        => Shell.Current.GoToAsync(nameof(Views.SettingsPage));

    [RelayCommand]
    private static Task NavigateToEstadisticasAsync()
        => Shell.Current.GoToAsync(nameof(Views.EstadisticasPage));

    [RelayCommand]
    private static Task NavigateToAboutAsync()
        => Shell.Current.GoToAsync(nameof(Views.AboutPage));
}
