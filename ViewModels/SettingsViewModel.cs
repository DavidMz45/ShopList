using ShopList.Models;
using ShopList.Services;
using System.Windows.Input;

namespace ShopList.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsModel Settings { get; set; }

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            Settings = DataService.Instance.Settings;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        async System.Threading.Tasks.Task SaveAsync()
        {
            DataService.Instance.Settings = Settings;
            await DataService.Instance.SaveAsync();
        }
    }
}
