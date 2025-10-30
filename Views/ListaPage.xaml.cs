using Microsoft.Maui.Controls;
using ShopList.ViewModels;
using ShopList.Models;
using System;

namespace ShopList.Views
{
    public partial class ListaPage : ContentPage
    {
        ListaViewModel vm;
        public ListaPage()
        {
            InitializeComponent();
            vm = new ListaViewModel();
            BindingContext = vm;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("productedit");
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await vm.SaveAsync();
            await DisplayAlert("Guardado", "Lista guardada correctamente.", "OK");
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button b && b.BindingContext is Product p)
            {
                // simple: navigate to productedit and prefill by setting DataService temp
                // For simplicity, open editor to add new or edit implemented in future
                await Shell.Current.GoToAsync("productedit");
            }
        }

        private async void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox cb && cb.BindingContext is Product p)
            {
                await vm.ToggleBoughtCommand.ExecuteAsync(p);
            }
        }

        private async void OnCompleteClicked(object sender, EventArgs e)
        {
            await vm.CompleteListCommand.ExecuteAsync(null);
            await DisplayAlert("Completado", "Los items comprados se movieron al historial.", "OK");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _ = vm.SaveAsync();
        }
    }
}
