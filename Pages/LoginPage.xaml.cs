// Pages/LoginPage.xaml.cs
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using ProyectoLite__8._0_.Services;
using ProyectoLite__8._0_.Models; // Aseg�rate de que apunte a Models.Usuario

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
                await DisplayAlert("Error", "Por favor, ingresa tu usuario y contrase�a.", "OK");
                return;
            }

            try
            {
               
                bool isAuthenticated = await _databaseService.AuthenticateUserAsync(username, password);

                if (isAuthenticated)
                {
                    await DisplayAlert("�xito", "Inicio de sesi�n exitoso.", "OK");
                    await Shell.Current.GoToAsync("//PrincipalPage");
                }
                else
                {
                    await DisplayAlert("Error", "Usuario o contrase�a incorrectos.", "OK");
                    EntryPassword.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurri� un error durante el inicio de sesi�n: {ex.Message}", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Este m�todo es llamado por el TappedGestureRecognizer en el Label "Reg�strate"
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            // Este m�todo es llamado por el TappedGestureRecognizer en el Label "�Olvidaste tu contrase�a?"
            await DisplayAlert("Recuperar Contrase�a", "Funcionalidad de recuperaci�n de contrase�a no implementada.", "OK");
        }
    }
}