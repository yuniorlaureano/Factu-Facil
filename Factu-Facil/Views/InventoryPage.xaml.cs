using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using FactuFacil.Models;
using FactuFacil.Services;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class InventoryPage : ContentPage
    {
        private HttpClientServiceBase<Inventory> httpClientServiceBase;

        public InventoryPage()
        {
            InitializeComponent();
            httpClientServiceBase = new HttpClientServiceBase<Inventory>(App.BackendUrl, UrlApiConstants.INVENTORY, App.AuthViewModel.Token);
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Inventory)layout.BindingContext;
            await Navigation.PushAsync(new InventoryAddPage(item));
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryAddPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadInventorys();
        }

        private async void LoadInventorys()
        {
            var inventory = await httpClientServiceBase.GetAsync();
            ItemsCollectionView.ItemsSource = inventory.ToList();
        }
    }
}