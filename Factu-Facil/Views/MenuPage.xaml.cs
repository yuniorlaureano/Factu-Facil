using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();


        }

        private async void btnCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientsPage());
        }

        private async void btnProducto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage());
        }

        private void btnFacturas_Clicked(object sender, EventArgs e)
        {

        }

        private void btnUsuario_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void btnIventario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {

        }

    }
}