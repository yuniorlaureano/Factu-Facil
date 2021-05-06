using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FactuFacil.Models;
using FactuFacil.Services;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class ClientsPage : ContentPage
    {
        private HttpClientServiceBase<Client> httpClientServiceBase;

        public ClientsPage()
        {
            InitializeComponent();
            httpClientServiceBase = new HttpClientServiceBase<Client>(App.BackendUrl, UrlApiConstants.CLIENT, App.AuthViewModel.Token);
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Client)layout.BindingContext;
            await Navigation.PushAsync(new ClientAddPage(item));
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientAddPage());
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadClients();
        }

        private async void LoadClients()
        {
            var clients = await httpClientServiceBase.GetAsync();
            ItemsCollectionView.ItemsSource = clients.ToList();
        }
    }
}