using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoLite__8._0_.Models;
using ProyectoLite__8._0_.Services;

namespace ProyectoLite__8._0_.Pages
{
    [QueryProperty(nameof(RecetaId), "RecetaId")]
    public partial class Principal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Receta> Recetas { get; set; } = new ObservableCollection<Receta>();
        public ObservableCollection<string> Categorias { get; set; } = new ObservableCollection<string>();

        private string _selectedCategory = "Todas";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    CargarRecetasPorCategoria();
                }
            }
        }

        public Principal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarDatosIniciales();
        }

        private async Task CargarDatosIniciales()
        {
            await _databaseService.InitializeAsync();

            var allRecetas = await _databaseService.GetRecetasAsync();
            var categoriasUnicas = allRecetas
                                    .Select(r => r.Categoria)
                                    .Distinct()
                                    .OrderBy(c => c)
                                    .ToList();

            Categorias.Clear();
            Categorias.Add("Todas");
            foreach (var categoria in categoriasUnicas)
            {
                Categorias.Add(categoria);
            }

            if (!Categorias.Contains(_selectedCategory))
            {
                _selectedCategory = "Todas";
            }
            OnPropertyChanged(nameof(SelectedCategory));

            CargarRecetasPorCategoria();
        }

        private async void CargarRecetasPorCategoria()
        {
            Recetas.Clear();
            var todasLasRecetas = await _databaseService.GetRecetasAsync();

            if (_selectedCategory == "Todas")
            {
                foreach (var receta in todasLasRecetas)
                {
                    Recetas.Add(receta);
                }
            }
            else
            {
                foreach (var receta in todasLasRecetas.Where(r => r.Categoria == _selectedCategory))
                {
                    Recetas.Add(receta);
                }
            }
        }

        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is string category)
            {
                SelectedCategory = category;
            }
        }

        private async void OnRecipeClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is ImageButton button && button.CommandParameter is int recetaId)
                {
                    await Shell.Current.GoToAsync($"Producto?RecetaId={recetaId}");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo navegar: {ex.Message}", "OK");
            }
        }
    }
}