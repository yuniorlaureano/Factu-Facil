using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FactuFacil.Models;
using FactuFacil.Views;
using FactuFacil.ViewModels;
using FactuFacil.Services;
using System.Collections.ObjectModel;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class ProductsPage : ContentPage
    {
        private HttpClientServiceBase<Product> httpClientServiceBase;

        public ProductsPage()
        {
            InitializeComponent();
            httpClientServiceBase = new HttpClientServiceBase<Product>(App.BackendUrl, UrlApiConstants.PRODUCT, App.AuthViewModel.Token);
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Product)layout.BindingContext;
            await Navigation.PushAsync(new ProductAddPage(item));
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
    }
}