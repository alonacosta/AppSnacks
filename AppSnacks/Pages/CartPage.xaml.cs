using AppSnacks.Models;
using AppSnacks.Services;
using AppSnacks.Validations;
using System.Collections.ObjectModel;

namespace AppSnacks.Pages;

public partial class CartPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    private bool _loginPageDisplayed = false;   

    private ObservableCollection<ShoppingCartItem> ItensCarrinhoCompra = new ObservableCollection<ShoppingCartItem>();

    public CartPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetItensCarrinhoCompra();
    }

    private async Task<IEnumerable<ShoppingCartItem>> GetItensCarrinhoCompra()
    {
        try
        {
            var usuarioId = Preferences.Get("userid", 0);
            var (itensCarrinhoCompra, errorMessage) = await
                     _apiService.GetItensCarrinhoCompra(usuarioId);

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                // Redirecionar para a p?gina de login
                await DisplayLoginPage();
                return Enumerable.Empty<ShoppingCartItem>();
            }

            if (itensCarrinhoCompra == null)
            {
                await DisplayAlert("Erro", errorMessage ?? "Não foi possivel obter os itens do carrinho de compra.", "OK");
                return Enumerable.Empty<ShoppingCartItem>();
            }

            ItensCarrinhoCompra.Clear();
            foreach (var item in itensCarrinhoCompra)
            {
                ItensCarrinhoCompra.Add(item);
            }

            CvCarrinho.ItemsSource = ItensCarrinhoCompra;
            AtualizaPrecoTotal(); // Atualizar o preco total ap?s atualizar os itens do carrinho

            //if (!ItensCarrinhoCompra.Any())
            //{
            //    return false;
            //}
            //return true;
            return ItensCarrinhoCompra;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            //return false;
            return Enumerable.Empty<ShoppingCartItem>();
        }
    }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;

        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }

    private void AtualizaPrecoTotal()
    {
        try
        {
            var precoTotal = ItensCarrinhoCompra.Sum(item => item.Price * item.Quantity);
            LblPrecoTotal.Text = precoTotal.ToString();
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"Ocorreu um erro ao atualizar o pre?o total: {ex.Message}", "OK");
        }
    }



    private void BtnDecrementar_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnIncrementar_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnDeletar_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnEditaEndereco_Clicked(object sender, EventArgs e)
    {

    }

    private void TapConfirmarPedido_Tapped(object sender, TappedEventArgs e)
    {

    }
}