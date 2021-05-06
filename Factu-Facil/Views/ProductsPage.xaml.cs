using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

using FactuFacil.Models;
using FactuFacil.Services;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class ProductsPage : ContentPage
    {
        private HttpClientServiceBase<Product> httpClientServiceBase;
        private bool isInventoryPage;

        public ProductsPage(bool isInventoryPage = false)
        {
            InitializeComponent();
            httpClientServiceBase = new HttpClientServiceBase<Product>(App.BackendUrl, UrlApiConstants.PRODUCT, App.AuthViewModel.Token);
            this.isInventoryPage = isInventoryPage;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            Product product = GetProductFromItemList(sender);

            if (!isInventoryPage)
            {
                await Navigation.PushAsync(new ProductAddPage(product));
                return;
            }

            Inventory inventory = new Inventory();
            inventory.Product = product;
            inventory.ProductId = product.Id;
            await Navigation.PushAsync(new InventoryAddPage(inventory));
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductAddPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await httpClientServiceBase.GetAsync();
            ItemsCollectionView.ItemsSource = products.ToList();
        }

        private Product GetProductFromItemList(object sender)
        {
            var layout = (BindableObject)sender;
            return (Product)layout.BindingContext;
        }
    }
}