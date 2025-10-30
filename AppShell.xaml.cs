using Microsoft.Maui.Controls;
using ShopList.Views;

namespace ShopList;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
        Dispatcher.Dispatch(async () => await GoToAsync(nameof(SplashPage)));
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(SplashPage), typeof(SplashPage));
        Routing.RegisterRoute(nameof(ListaPage), typeof(ListaPage));
        Routing.RegisterRoute(nameof(ProductoEditarCrearPage), typeof(ProductoEditarCrearPage));
        Routing.RegisterRoute(nameof(CategoriasPage), typeof(CategoriasPage));
        Routing.RegisterRoute(nameof(PlantillasPage), typeof(PlantillasPage));
        Routing.RegisterRoute(nameof(HistorialPage), typeof(HistorialPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(EstadisticasPage), typeof(EstadisticasPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
    }
}
