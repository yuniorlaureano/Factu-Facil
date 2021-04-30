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
    public partial class ProductAddPage : ContentPage
    {
        public Product Product { get; set; }
        private HttpClientServiceBase<Product> httpClientServiceBase;

        public ProductAddPage()
        {
            Init(new Product());
        }

        public ProductAddPage(Product product)
        {
            Init(Product);
            InitializeFilds(product);
        }

        private void Init(Product product)
        {
            InitializeComponent();
            Product = product;
            BindingContext = Product;
            HideDeleteButton(Product);
            httpClientServiceBase = new HttpClientServiceBase<Product>(App.BackendUrl, "product", App.AuthViewModel.Token);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Product.Id == Guid.Empty)
            {
                await httpClientServiceBase.AddAsync(Product);
            }
            else
            {
                await httpClientServiceBase.UpdateAsync(Product);
            }
            
            ClearFields();
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void InitializeFilds(Product product)
        {
            Product = product;
            txtCode.SetValue(Editor.TextProperty, product.Code);
            txtName.SetValue(Editor.TextProperty, product.Name);
            txtDescription.SetValue(Editor.TextProperty, product.Description);
            txtPurchasePrice.SetValue(Editor.TextProperty, product.PurchasePrice);
            txtSalePrice.SetValue(Editor.TextProperty, product.SalePrice);
        }

        private void ClearFields()
        {
            Product = new Product();
            txtCode.ClearValue(Editor.TextProperty);
            txtName.ClearValue(Editor.TextProperty);
            txtDescription.ClearValue(Editor.TextProperty);
            txtPurchasePrice.ClearValue(Editor.TextProperty);
            txtSalePrice.ClearValue(Editor.TextProperty);
        }

        private void HideDeleteButton(Product product)
        {
            if (NoProductSet() && this.ToolbarItems.Contains(ToolItemDelete))
            {
                this.ToolbarItems.Remove(ToolItemDelete);
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (!NoProductSet())
            {
                bool isDelete = await DisplayAlert("Borrar", "Desea borrar el registro", "Sí", "No");
                if (isDelete)
                {
                    await httpClientServiceBase.DeleteAsync(Product.Id);
                    ClearFields();
                    await Navigation.PopAsync();
                }
                
            }
        }

        private bool NoProductSet() => Product?.Id == Guid.Empty;
    }
}