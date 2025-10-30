using Microsoft.Maui.Controls;
using ShopList.Views;

namespace ShopList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("productedit", typeof(ProductoEditarCrearPage));
        }
    }
}
