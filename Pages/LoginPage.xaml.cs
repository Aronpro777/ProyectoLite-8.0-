// Pages/LoginPage.xaml.cs
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using ProyectoLite__8._0_.Services;
using ProyectoLite__8._0_.Models; // Asegúrate de que apunte a Models.Usuario

namespace ProyectoLite__8._0_.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public LoginPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text ?? string.Empty;
            string password = EntryPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Por favor, ingresa tu usuario y contraseña.", "OK");
                return;
            }

            try
            {
               
                bool isAuthenticated = await _databaseService.AuthenticateUserAsync(username, password);

                if (isAuthenticated)
                {
                    await DisplayAlert("Éxito", "Inicio de sesión exitoso.", "OK");
                    await Shell.Current.GoToAsync("//PrincipalPage");
                }
                else
                {
                    await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
                    EntryPassword.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error durante el inicio de sesión: {ex.Message}", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Este método es llamado por el TappedGestureRecognizer en el Label "Regístrate"
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            // Este método es llamado por el TappedGestureRecognizer en el Label "¿Olvidaste tu contraseña?"
            await DisplayAlert("Recuperar Contraseña", "Funcionalidad de recuperación de contraseña no implementada.", "OK");
        }
    }
}