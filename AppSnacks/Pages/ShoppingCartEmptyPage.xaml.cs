namespace AppSnacks.Pages;

public partial class ShoppingCartEmptyPage : ContentPage
{
	public ShoppingCartEmptyPage()
	{
		InitializeComponent();
	}

    private async void BtnRetornar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
}