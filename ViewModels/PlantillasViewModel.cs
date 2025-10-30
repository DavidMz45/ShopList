using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ShopList.Models;
using ShopList.Repositories;

namespace ShopList.ViewModels;

public partial class PlantillasViewModel : BaseViewModel
{
    private readonly ITemplateRepository _templateRepository;
    private readonly IProductRepository _productRepository;

    public ObservableCollection<TemplateList> Templates { get; } = new();

    [ObservableProperty]
    private string templateName = string.Empty;

    [ObservableProperty]
    private TemplateList? selectedTemplate;

    public PlantillasViewModel(ITemplateRepository templateRepository, IProductRepository productRepository)
    {
        _templateRepository = templateRepository;
        _productRepository = productRepository;
        Title = "Plantillas";
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
            var templates = await _templateRepository.GetAllAsync();
            Templates.Clear();
            foreach (var template in templates)
            {
                Templates.Add(template);
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
        TemplateName = string.Empty;
        SelectedTemplate = null;
    }

    [RelayCommand]
    private void Select(TemplateList template)
    {
        SelectedTemplate = template;
        TemplateName = template.Name;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            await Shell.Current.DisplayAlert("Validación", "El nombre es obligatorio.", "Aceptar");
            return;
        }

        var products = await _productRepository.GetPendingAsync();
        if (!products.Any())
        {
            await Shell.Current.DisplayAlert("Información", "No hay productos para guardar en la plantilla.", "Aceptar");
            return;
        }

        try
        {
            var template = SelectedTemplate ?? new TemplateList();
            template.Name = TemplateName.Trim();
            template.Items = products.Select(p => new Product
            {
                Name = p.Name,
                Quantity = p.Quantity,
                CategoryId = p.CategoryId,
                IsPurchased = false
            }).ToList();

            await _templateRepository.SaveAsync(template);
            await LoadAsync();
            StartNew();
            await Shell.Current.DisplayAlert("Éxito", "Plantilla guardada.", "Aceptar");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private async Task ApplyAsync(TemplateList template)
    {
        try
        {
            var confirm = await Shell.Current.DisplayAlert("Confirmación", $"¿Cargar la plantilla {template.Name}?", "Sí", "No");
            if (!confirm)
            {
                return;
            }

            foreach (var item in template.Items)
            {
                var product = new Product
                {
                    Name = item.Name,
                    Quantity = item.Quantity,
                    CategoryId = item.CategoryId,
                    IsPurchased = false
                };

                await _productRepository.SaveAsync(product);
            }

            await Shell.Current.DisplayAlert("Éxito", "Plantilla aplicada a la lista.", "Aceptar");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    [RelayCommand]
    private async Task DeleteAsync(TemplateList template)
    {
        try
        {
            var confirm = await Shell.Current.DisplayAlert("Confirmación", $"¿Eliminar la plantilla {template.Name}?", "Sí", "No");
            if (!confirm)
            {
                return;
            }

            await _templateRepository.DeleteAsync(template.Id);
            await LoadAsync();
            if (SelectedTemplate?.Id == template.Id)
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
