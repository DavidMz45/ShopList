using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ShopList.Services;

namespace ShopList.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;

    public Array ThemeOptions { get; } = Enum.GetValues(typeof(AppTheme));
    public Array OrderOptions { get; } = Enum.GetValues(typeof(ListOrderOption));

    [ObservableProperty]
    private AppTheme selectedTheme;

    [ObservableProperty]
    private ListOrderOption selectedOrder;

    [ObservableProperty]
    private bool confirmDeletion;

    [ObservableProperty]
    private bool confirmClear;

    [ObservableProperty]
    private double fontMultiplier = 1.0;

    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        Title = "Configuración";
        LoadSettings();
    }

    private void LoadSettings()
    {
        SelectedTheme = _settingsService.GetTheme();
        SelectedOrder = _settingsService.GetListOrder();
        ConfirmDeletion = _settingsService.GetConfirmDeletion();
        ConfirmClear = _settingsService.GetConfirmClear();
        FontMultiplier = _settingsService.GetFontSizeMultiplier();
        ApplyFontMultiplier();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        _settingsService.SetTheme(SelectedTheme);
        _settingsService.SetListOrder(SelectedOrder);
        _settingsService.SetConfirmDeletion(ConfirmDeletion);
        _settingsService.SetConfirmClear(ConfirmClear);
        _settingsService.SetFontSizeMultiplier(FontMultiplier);

        Application.Current!.UserAppTheme = SelectedTheme;
        ApplyFontMultiplier();

        await Shell.Current.DisplayAlert("Éxito", "Configuraciones guardadas.", "Aceptar");
    }

    private void ApplyFontMultiplier()
    {
        var body = 16d * FontMultiplier;
        var title = 22d * FontMultiplier;
        Application.Current!.Resources["BodyFontSize"] = body;
        Application.Current.Resources["TitleFontSize"] = title;
    }
}
