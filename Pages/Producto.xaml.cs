using ProyectoLite__8._0_.Models;
using ProyectoLite__8._0_.Services;

namespace ProyectoLite__8._0_.Pages
{
    [QueryProperty(nameof(RecetaId), "RecetaId")]
    public partial class Producto : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Receta _receta;

        public int RecetaId
        {
            set => CargarReceta(value);
        }

        public Receta Receta
        {
            get => _receta;
            set
            {
                _receta = value;
                OnPropertyChanged();
            }
        }

        public Producto(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = this;
        }

        private async void CargarReceta(int recetaId)
        {
            try
            {
                Receta = await _databaseService.GetRecetaByIdAsync(recetaId);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar la receta: {ex.Message}", "OK");
            }
        }
    }
}