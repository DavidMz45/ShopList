using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using ShopList.Data;
using ShopList.Repositories;
using ShopList.Services;

namespace ShopList.ViewModels;

public partial class SplashViewModel : BaseViewModel
{
    private readonly Database _database;
    private readonly ITemplateRepository _templateRepository;
    private readonly ISettingsService _settingsService;

    public SplashViewModel(Database database, ITemplateRepository templateRepository, ISettingsService settingsService)
    {
        _database = database;
        _templateRepository = templateRepository;
        _settingsService = settingsService;
        Title = "ShopList";
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            SetBusy(true);
            await _database.InitializeAsync();
            await _templateRepository.LoadFromFileAsync();
            Application.Current!.UserAppTheme = _settingsService.GetTheme();
            await Task.Delay(1200);
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync("//ListaPage");
            });
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Aceptar");
            });
        }
        finally
        {
            SetBusy(false);
        }
    }
}
