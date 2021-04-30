using System;
using System.ComponentModel;
using Xamarin.Forms;
using FactuFacil.Services;
using FactuFacil.Models;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class ClientAddPage : ContentPage
    {
        public Client Client { get; set; }
        private HttpClientServiceBase<Client> httpClientServiceBase;

        public ClientAddPage()
        {
            Init(new Client());
        }

        public ClientAddPage(Client client)
        {
            Init(client);
            InitializeFilds(client);
        }

        private void Init(Client client)
        {
            InitializeComponent();
            Client = client;
            BindingContext = Client;
            HideDeleteButton(Client);
            httpClientServiceBase = new HttpClientServiceBase<Client>(App.BackendUrl, UrlApiConstants.CLIENT, App.AuthViewModel.Token);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (NoclientSet())
            {
                await httpClientServiceBase.AddAsync(Client);
            }
            else
            {
                await httpClientServiceBase.UpdateAsync(Client);
            }
            
            ClearFields();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void InitializeFilds(Client client)
        {
            Client = client;
            txtName.SetValue(Editor.TextProperty, client.Name);
            txtLastName.SetValue(Editor.TextProperty, client.LastName);
            txtIdentificationCard.SetValue(Editor.TextProperty, client.IdentificationCard);
            txtPhone.SetValue(Editor.TextProperty, client.Phone);
            txtAddress.SetValue(Editor.TextProperty, client.Address);
        }

        private void ClearFields()
        {
            Client = new Client();
            txtName.ClearValue(Editor.TextProperty);
            txtLastName.ClearValue(Editor.TextProperty);
            txtIdentificationCard.ClearValue(Editor.TextProperty);
            txtPhone.ClearValue(Editor.TextProperty);
            txtAddress.ClearValue(Editor.TextProperty);
        }

        private void HideDeleteButton(Client client)
        {
            if (NoclientSet() && this.ToolbarItems.Contains(ToolItemDelete))
            {
                this.ToolbarItems.Remove(ToolItemDelete);
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (!NoclientSet())
            {
                bool isDelete = await DisplayAlert("Borrar", "Desea borrar el registro", "Sí", "No");
                if (isDelete)
                {
                    await httpClientServiceBase.DeleteAsync(Client.Id);
                    ClearFields();
                    await Navigation.PopAsync();
                }
                
            }
        }

        private bool NoclientSet() => Client?.Id == Guid.Empty;
    }
}