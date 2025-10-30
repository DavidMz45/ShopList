using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ShopList.Models;
using ShopList.Repositories;

namespace ShopList.ViewModels;

public partial class CategoriasViewModel : BaseViewModel
{
    private readonly ICategoryRepository _categoryRepository;

    public ObservableCollection<Category> Categories { get; } = new();

    [ObservableProperty]
    private string categoryName = string.Empty;

    [ObservableProperty]
    private Category? selectedCategory;

    public CategoriasViewModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        Title = "Categorías";
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
            var categories = await _categoryRepository.GetAllAsync();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
        finally
        {
            SetBusy(false);
        }
    }

    [RelayCommand]
    private void StartNew()
    {
        CategoryName = string.Empty;
        SelectedCategory = null;
    }

    [RelayCommand]
    private void Select(Category category)
    {
        SelectedCategory = category;
        CategoryName = category.Name;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(CategoryName))
        {
            await Shell.Current.DisplayAlert("Validación", "El nombre es obligatorio.", "Aceptar");
            return;
        }

        try
        {
            var category = SelectedCategory ?? new Category();
            category.Name = CategoryName.Trim();
            await _categoryRepository.SaveAsync(category);
            await LoadAsync();
            StartNew();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private async Task DeleteAsync(Category category)
    {
        try
        {
            var confirm = await Shell.Current.DisplayAlert("Confirmación", $"¿Eliminar la categoría {category.Name}?", "Sí", "No");
            if (!confirm)
            {
                return;
            }

            await _categoryRepository.DeleteAsync(category.Id);
            await LoadAsync();
            if (SelectedCategory?.Id == category.Id)
            {
                StartNew();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }
}
