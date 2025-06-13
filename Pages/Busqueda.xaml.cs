namespace ProyectoLite__8._0_.Pages;

public partial class Busqueda : ContentPage
{
	public Busqueda()
	{
		InitializeComponent();
	}
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PrincipalPage");
    }
    
}