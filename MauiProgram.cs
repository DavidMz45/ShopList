using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using ShopList.Data;
using ShopList.Repositories;
using ShopList.Services;
using ShopList.ViewModels;
using ShopList.Views;

namespace ShopList;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<Database>();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
        builder.Services.AddSingleton<ITemplateRepository, TemplateRepository>();
        builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        builder.Services.AddTransient<SplashViewModel>();
        builder.Services.AddTransient<ListaViewModel>();
        builder.Services.AddTransient<ProductoEditViewModel>();
        builder.Services.AddTransient<CategoriasViewModel>();
        builder.Services.AddTransient<PlantillasViewModel>();
        builder.Services.AddTransient<HistorialViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<EstadisticasViewModel>();
        builder.Services.AddSingleton<AboutViewModel>();

        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<ListaPage>();
        builder.Services.AddTransient<ProductoEditarCrearPage>();
        builder.Services.AddTransient<CategoriasPage>();
        builder.Services.AddTransient<PlantillasPage>();
        builder.Services.AddTransient<HistorialPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<EstadisticasPage>();
        builder.Services.AddTransient<AboutPage>();

        return builder.Build();
    }
}
