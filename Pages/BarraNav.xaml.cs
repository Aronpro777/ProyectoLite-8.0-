namespace ProyectoLite__8._0_.Pages;

public partial class BarraNav : ContentView
{
    public BarraNav()
    {
        InitializeComponent();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PrincipalPage");
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Busqueda");
    }
}