using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ShopList.Helpers;
using ShopList.Models;
using ShopList.Repositories;

namespace ShopList.ViewModels;

public partial class ProductoEditViewModel : BaseViewModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ObservableCollection<Category> Categories { get; } = new();

    [ObservableProperty]
    private int productId;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private int quantity = 1;

    [ObservableProperty]
    private Category? selectedCategory;

    [ObservableProperty]
    private string validationMessage = string.Empty;

    [ObservableProperty]
    private bool isPurchased;

    public bool IsEdit => ProductId > 0;

    public ProductoEditViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        Title = "Producto";
    }

    public async Task InitializeAsync(int? productId)
    {
        await LoadCategoriesAsync();
        if (productId.HasValue && productId.Value > 0)
        {
            await LoadProductAsync(productId.Value);
        }
        else
        {
            ProductId = 0;
            Name = string.Empty;
            Quantity = 1;
            SelectedCategory = Categories.Count > 0 ? Categories[0] : null;
            IsPurchased = false;
        }
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        Categories.Clear();
        foreach (var category in categories)
        {
            Categories.Add(category);
        }

        if (SelectedCategory == null && Categories.Count > 0)
        {
            SelectedCategory = Categories[0];
        }
    }

    private async Task LoadProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            await Shell.Current.DisplayAlert("Error", "Producto no encontrado", "Aceptar");
            await Shell.Current.GoToAsync("..");
            return;
        }

        ProductId = product.Id;
        Name = product.Name;
        Quantity = product.Quantity;
        SelectedCategory = Categories.FirstOrDefault(c => c.Id == product.CategoryId);
        IsPurchased = product.IsPurchased;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidationRules.ValidateProductName(Name, out var message))
        {
            ValidationMessage = message;
            return;
        }

        if (!ValidationRules.ValidateQuantity(Quantity, out message))
        {
            ValidationMessage = message;
            return;
        }

        if (SelectedCategory == null)
        {
            ValidationMessage = "Seleccione una categoría.";
            return;
        }

        try
        {
            ValidationMessage = string.Empty;
            var product = new Product
            {
                Id = ProductId,
                Name = Name,
                Quantity = Quantity,
                CategoryId = SelectedCategory.Id,
                IsPurchased = IsPurchased
            };

            await _productRepository.SaveAsync(product);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private static Task CancelAsync()
        => Shell.Current.GoToAsync("..");
}
