using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ShopList.Services;
using ShopList.ViewModels;

namespace ShopList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { });

            // Register services and viewmodels (simple)
            builder.Services.AddSingleton(DataService.Instance);
            builder.Services.AddTransient<SplashViewModel>();
            builder.Services.AddTransient<ViewModels.ListaViewModel>();
            builder.Services.AddTransient<ViewModels.ProductoEditarCrearViewModel>();
            builder.Services.AddTransient<ViewModels.CategoriasViewModel>();
            builder.Services.AddTransient<ViewModels.PlantillasViewModel>();
            builder.Services.AddTransient<ViewModels.HistorialViewModel>();
            builder.Services.AddTransient<ViewModels.SettingsViewModel>();
            builder.Services.AddTransient<ViewModels.EstadisticasViewModel>();

            return builder.Build();
        }
    }
}
