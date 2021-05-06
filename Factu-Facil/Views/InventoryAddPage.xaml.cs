using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FactuFacil.Services;

using FactuFacil.Models;
using Xamarin.Essentials;

namespace FactuFacil.Views
{
    [DesignTimeVisible(false)]
    public partial class InventoryAddPage : ContentPage
    {
        public Inventory Inventory { get; set; }
        private HttpClientServiceBase<Inventory> httpClientServiceBase;

        public InventoryAddPage()
        {
            Init(new Inventory());
        }

        public InventoryAddPage(Inventory inventory)
        {
            Init(inventory);
            InitializeFilds(inventory);
        }

        private void Init(Inventory inventory)
        {
            InitializeComponent();
            Inventory = inventory;
            BindingContext = Inventory;
            HideDeleteButton();
            httpClientServiceBase = new HttpClientServiceBase<Inventory>(App.BackendUrl, UrlApiConstants.INVENTORY, App.AuthViewModel.Token);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Inventory.Id == Guid.Empty)
            {
                await httpClientServiceBase.AddAsync(Inventory);
            }
            else
            {
                await httpClientServiceBase.UpdateAsync(Inventory);
            }
            
            ClearFields();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void InitializeFilds(Inventory inventory)
        {
            Inventory = inventory;
            txtCode.SetValue(Editor.TextProperty, inventory.Product.Code);
            txtName.SetValue(Editor.TextProperty, inventory.Product.Name);
            txtAmount.SetValue(Editor.TextProperty, inventory.Amount);
        }

        private void ClearFields()
        {
            Inventory = new Inventory();
            txtCode.ClearValue(Editor.TextProperty);
            txtName.ClearValue(Editor.TextProperty);
            txtAmount.ClearValue(Editor.TextProperty);
        }

        private void HideDeleteButton()
        {
            if (NoInventorySet() && this.ToolbarItems.Contains(ToolItemDelete))
            {
                this.ToolbarItems.Remove(ToolItemDelete);
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (!NoInventorySet())
            {
                bool isDelete = await DisplayAlert("Borrar", "Desea borrar el registro", "Sí", "No");
                if (isDelete)
                {
                    await httpClientServiceBase.DeleteAsync(Inventory.Id);
                    ClearFields();
                    await Navigation.PopAsync();
                }
                
            }
        }

        private bool NoInventorySet() => Inventory?.Id == Guid.Empty;

        private async void BuscarProducto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage(true));
        }
    }
}