namespace AppSnacks.Pages;

public partial class ConfirmedOrderPage : ContentPage
{
	public ConfirmedOrderPage()
	{
		InitializeComponent();
	}

    private async void BtnRetornar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}