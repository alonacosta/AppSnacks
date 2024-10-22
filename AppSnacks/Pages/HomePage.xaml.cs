using AppSnacks.Models;
using AppSnacks.Services;
using AppSnacks.Validations;
using System.ComponentModel.DataAnnotations;

namespace AppSnacks.Pages;

public partial class HomePage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    private bool _loginPageDisplayed = false;

    public HomePage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        LblNomeUsuario.Text = "Olá, " + Preferences.Get("username", string.Empty);
        _validator = validator;
       
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetListaCategorias();
        await GetMaisVendidos();
        await GetPopulares();
    }

    private async Task<IEnumerable<Category>> GetListaCategorias()
    {
        try
        {
            var (categorias, errorMessage) = await _apiService.GetCategorias();

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return Enumerable.Empty<Category>();
            }

            if (categorias == null)
            {
                await DisplayAlert("Erro", errorMessage ?? "Não foi possível obter as categorias.", "OK");
                return Enumerable.Empty<Category>();
            }

            CvCategorias.ItemsSource = categorias;
            return categorias;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            return Enumerable.Empty<Category>();
        }
    }

    private async Task<IEnumerable<Product>> GetMaisVendidos()
    {
        try
        {
            var (produtos, errorMessage) = await _apiService.GetProdutos("maisvendido", string.Empty);

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return Enumerable.Empty<Product>();
            }

            if (produtos == null)
            {
                await DisplayAlert("Erro", errorMessage ?? "Não foi possível obter as categorias.", "OK");
                return Enumerable.Empty<Product>();
            }

            CvMaisVendidos.ItemsSource = produtos;
            return produtos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            return Enumerable.Empty<Product>();
        }
    }

    private async Task<IEnumerable<Product>> GetPopulares()
    {
        try
        {
            var (produtos, errorMessage) = await _apiService.GetProdutos("popular", string.Empty);

            if (errorMessage == "Unauthorized" && !_loginPageDisplayed)
            {
                await DisplayLoginPage();
                return Enumerable.Empty<Product>();
            }

            if (produtos == null)
            {
                await DisplayAlert("Erro", errorMessage ?? "Não foi possível obter as categorias.", "OK");
                return Enumerable.Empty<Product>();
            }
            CvPopulares.ItemsSource = produtos;
            return produtos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
            return Enumerable.Empty<Product>();
        }
    }

    private async Task DisplayLoginPage()
    {
        _loginPageDisplayed = true;
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }

    private void CvCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void CvMaisVendidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void CvPopulares_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}