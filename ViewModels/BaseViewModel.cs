using CommunityToolkit.Mvvm.ComponentModel;

namespace ShopList.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string title = string.Empty;

    public bool IsNotBusy => !IsBusy;

    protected void SetBusy(bool value)
    {
        IsBusy = value;
        OnPropertyChanged(nameof(IsNotBusy));
    }
}
