using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using ProyectoLite__8._0_.Models; // Add this for the User model
using ProyectoLite__8._0_.Services;

namespace ProyectoLite__8._0_.Pages
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public RegisterPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text ?? string.Empty; // Fix for CS8600
            string email = EntryEmail.Text ?? string.Empty;     // Fix for CS8600
            string password = EntryPassword.Text ?? string.Empty; // Fix for CS8600
            string confirmPassword = EntryConfirmPassword.Text ?? string.Empty; // Fix for CS8600

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                return;
            }

            // Check if user or email already exists using the correct method name
            // CORRECTION: GetUserByUsernameAsync instead of GetUserUsuarioAsync
            var existingUser = await _databaseService.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                await DisplayAlert("Error", "El nombre de usuario ya está en uso.", "OK");
                return;
            }

            // You might want to check for existing email too, if you have a method for it.
            // var existingEmail = await _databaseService.GetUserByEmailAsync(email); // If you add this method

            var newUser = new Usuario
            {
                NombreUsuario = username,
                CorreoElectronico = email,
                Contrasena = password // Password will be hashed inside RegisterUserAsync
            };

            try
            {
                // CORRECTION: RegisterUserAsync instead of SaveUsuarioAsync
                bool registered = await _databaseService.RegisterUserAsync(newUser);

                if (registered)
                {
                    await DisplayAlert("Éxito", "Registro exitoso. Ahora puedes iniciar sesión.", "OK");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    // This else block might be hit if RegisterUserAsync returns false for other reasons
                    // (e.g., email already exists, if you add that check).
                    await DisplayAlert("Error", "No se pudo registrar el usuario. Intenta de nuevo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error durante el registro: {ex.Message}", "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}