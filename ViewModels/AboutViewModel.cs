using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ShopList.ViewModels;

public partial class AboutViewModel : BaseViewModel
{
    [ObservableProperty]
    private string version = string.Empty;

    [ObservableProperty]
    private string author = "Equipo ShopList";

    [ObservableProperty]
    private string description = "Aplicación para gestionar tu lista de compras diaria.";

    public AboutViewModel()
    {
        Title = "Acerca de";
        Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
    }
}
