using AppSnacks.Models;
using AppSnacks.Services;
using AppSnacks.Validations;

namespace AppSnacks.Pages;

public partial class OrdersPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    private bool _loginPageDisplayed = false;

    public OrdersPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetListaPedidos();
    }

    private async Task GetListaPedidos()
    {
        try
        {
            var (pedidos, errorMessage) = await _apiService.GetPedidosPorUsuario(Preferences.Get("userid", 0));

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return;
            }
            if (errorMessage == "NotFound")
            {
                await DisplayAlert("Aviso", "N o existem pedidos para o cliente.", "OK");
                return;
            }
            if (pedidos is null)
            {
                await DisplayAlert("Erro", errorMessage ?? "N o foi poss vel obter pedidos.", "OK");
                return;
            }
            else
            {
                CvPedidos.ItemsSource = pedidos;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Erro", "Ocorreu um erro ao obter os pedidos. Tente novamente mais tarde.", "OK");
        }
    }


    private void CvPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as OrderPerUser;

        if (selectedItem == null) return;

        Navigation.PushAsync(new OrderDetailsPage(selectedItem.Id,
                                                    selectedItem.Total,
                                                    _apiService,
                                                    _validator));

        ((CollectionView)sender).SelectedItem = null;
    }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}