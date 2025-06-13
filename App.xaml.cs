namespace ProyectoLite__8._0_
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Establece AppShell como la página principal de la aplicación.
            // AppShell manejará la navegación a LoginPage inicialmente.
            MainPage = new AppShell();
        }
    }
}
