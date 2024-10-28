using AppSnacks.Services;
using AppSnacks.Validations;

namespace AppSnacks.Pages;

public partial class OrderDetailsPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    private bool _loginPageDisplayed = false;

    public OrderDetailsPage(int pedidoId,
                              decimal precoTotal, ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
        LblPrecoTotal.Text = " R$" + precoTotal;

        GetPedidoDetalhe(pedidoId);
    }

    private async void GetPedidoDetalhe(int pedidoId)
    {
        try
        {
            var (pedidoDetalhes, errorMessage) = await _apiService.GetPedidoDetalhes(pedidoId);

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return;
            }

            if (pedidoDetalhes is null)
            {
                await DisplayAlert("Erro", errorMessage ?? "N�o foi poss�vel obter detalhes do pedido.", "OK");
                return;
            }
            else
            {
                CvPedidoDetalhes.ItemsSource = pedidoDetalhes;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Erro", "Ocorreu um erro ao obter os detalhes. Tente novamente mais tarde.", "OK");
        }
    }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}


