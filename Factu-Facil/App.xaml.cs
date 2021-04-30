using Xamarin.Forms;
using FactuFacil.Models;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Authentication;
using FactuFacil.Views;

namespace FactuFacil
{
    public partial class App : Application
    {
        //public static string BackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://factufacil.azurewebsites.net";
        public static string BackendUrl = "https://factufacil.azurewebsites.net";
        public static AuthViewModel AuthViewModel { get; set; } = new AuthViewModel();

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            try
            {
                AuthViewModel = await GetToken();
            }
            catch (Exception)
            {
                new NavigationPage(new UserLoginPage());
            }
        }

        protected override void OnSleep()
        {

        }

        protected async override void OnResume()
        {
            try
            {
                AuthViewModel = await GetToken();
            }
            catch (Exception)
            {
                new NavigationPage(new UserLoginPage());
            }
        }

        public static async void AddToken(AuthViewModel auth, string tokenName = "jwt-token")
        {
            string serializedJsonUser = JsonConvert.SerializeObject(auth);
            await SecureStorage.SetAsync(tokenName, serializedJsonUser);
        }

        public static async Task<AuthViewModel> GetToken(string tokenName = "jwt-token")
        {
            AuthViewModel auth = new AuthViewModel();
            try
            {
                string jsonUser = await SecureStorage.GetAsync(tokenName);
                auth = JsonConvert.DeserializeObject<AuthViewModel>(jsonUser);
                if (string.IsNullOrWhiteSpace(auth.Token))
                {
                    throw new AuthenticationException("Usuario no logueado");
                }
            }
            catch (Exception)
            {
                throw new AuthenticationException("Usuario no logueado");
            }

            return auth;
        }
    }
}
