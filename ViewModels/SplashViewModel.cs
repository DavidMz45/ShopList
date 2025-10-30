using System.Threading.Tasks;
using System.Windows.Input;
using ShopList.Services;
using Microsoft.Maui.Controls;

namespace ShopList.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; }

        public SplashViewModel()
        {
            LoadCommand = new Command(async () => await LoadAsync());
        }

        private async Task LoadAsync()
        {
            IsBusy = true;
            await DataService.Instance.LoadAsync();
            await Task.Delay(700);
            IsBusy = false;
            await Shell.Current.GoToAsync("//lista");
        }
    }
}
