using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ProyectoLite__8._0_.Models;
using ProyectoLite__8._0_.Services;

namespace ProyectoLite__8._0_.Pages
{
    public partial class Principal : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Receta> Recetas { get; set; } = new ObservableCollection<Receta>();

        public Principal(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadRecipes();
        }

        private async Task LoadRecipes()
        {
            Recetas.Clear();
            var recipes = await _databaseService.GetRecetasAsync();
            foreach (var recipe in recipes)
            {
                Recetas.Add(recipe);
            }
        }

        private async void OnRecipeTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is int recipeId)
            {
                await Shell.Current.GoToAsync($"Producto?RecetaId={recipeId}");
            }
        }
    }
}