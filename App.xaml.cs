using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ShopList.Data;
using ShopList.Services;

namespace ShopList;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;
    private readonly Database _database;

    public App(ISettingsService settingsService, Database database)
    {
        InitializeComponent();
        _settingsService = settingsService;
        _database = database;

        Task.Run(() => _database.InitializeAsync()).Wait();
        ApplyTheme();
        ApplyFontMultiplier();

        MainPage = new AppShell();
    }

    private void ApplyTheme()
    {
        UserAppTheme = _settingsService.GetTheme();
    }

    private void ApplyFontMultiplier()
    {
        var multiplier = _settingsService.GetFontSizeMultiplier();
        Resources["BodyFontSize"] = 16d * multiplier;
        Resources["TitleFontSize"] = 22d * multiplier;
    }
}
