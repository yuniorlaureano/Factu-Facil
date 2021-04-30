using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FactuFacil.Models;
using FactuFacil.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FactuFacil.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class UserLoginPage : ContentPage
    {
        public UserViewModel User { get; set; } = new UserViewModel();
        UserService userService;

        public UserLoginPage()
        {
            InitializeComponent();
            BindingContext = User;
            userService = new UserService(null);
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.AuthViewModel = await userService.Authenticate(User);
                App.AddToken(App.AuthViewModel);
                await Navigation.PushAsync(new MenuPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Info", ex.Message, "Tray Again", "Ok");
            }            
        }
    }
}