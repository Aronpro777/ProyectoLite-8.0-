using ProyectoLite__8._0_.Pages;
namespace ProyectoLite__8._0_
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar las rutas de las páginas para la navegación.
            // Es vital que los nombres de las rutas coincidan con los 'nameof()' de las clases de página.
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(Principal), typeof(Principal));
            Routing.RegisterRoute(nameof(Producto), typeof(Producto)); // ¡Importante: Registrar la página Producto!
        }
    }
}
