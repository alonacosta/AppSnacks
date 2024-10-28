
using AppSnacks.Models;
using AppSnacks.Services;
using AppSnacks.Validations;

namespace AppSnacks.Pages;

public partial class FavouritePage : ContentPage
{
    private readonly FavouriteService _favoritosService;
    private readonly ApiService _apiService;
    private readonly IValidator _validator;

    public FavouritePage(ApiService apiService, IValidator validator)
	{
		InitializeComponent();
        _favoritosService = new FavouriteService();
        _apiService = apiService;
        _validator = validator;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetProdutosFavoritos();
    }
    private async Task GetProdutosFavoritos()
    {
        try
        {
            var produtosFavoritos = await _favoritosService.ReadAllAsync();

            if (produtosFavoritos is null || produtosFavoritos.Count == 0)
            {
                CvProdutos.ItemsSource = null;//limpa a lista atual
                LblAviso.IsVisible = true; //mostra o aviso
            }
            else
            {
                CvProdutos.ItemsSource = produtosFavoritos;
                LblAviso.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
        }
    }

    private void CvProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as FavouriteProduct;

        if (currentSelection == null) return;

        Navigation.PushAsync(new ProductDetailsPage(currentSelection.ProductId,
                                                     currentSelection.Name!,
                                                     _apiService, _validator));

        ((CollectionView)sender).SelectedItem = null;
    }

}
