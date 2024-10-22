using AppSnacks.Services;
using AppSnacks.Validations;


namespace AppSnacks.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly IValidator _validator;
    public RegisterPage(ApiService apiService, IValidator validator)
    {
        InitializeComponent();
        _apiService = apiService;
        _validator = validator;
    }

    private async void BtnSignup_Clicked(object sender, EventArgs e)
    {
        if (await _validator.Validate(EntNome.Text, EntEmail.Text, EntPhone.Text, EntPassword.Text))
        {

            var response = await _apiService.RegistrarUsuario(EntNome.Text, EntEmail.Text,
                                                          EntPhone.Text, EntPassword.Text);

            if (!response.HasError)
            {
                await DisplayAlert("Aviso", "Sua conta foi criada com sucesso !!", "OK");
                await Navigation.PushAsync(new LoginPage(_apiService, _validator));
            }
            else
            {
                await DisplayAlert("Erro", "Algo deu errado!!!", "Cancelar");
            }
        }
        else
        {
            string mensagemErro = "";
            mensagemErro += _validator.NameErro != null ? $"\n- {_validator.NameErro}" : "";
            mensagemErro += _validator.EmailErro != null ? $"\n- {_validator.EmailErro}" : "";
            mensagemErro += _validator.PhoneErro != null ? $"\n- {_validator.PhoneErro}" : "";
            mensagemErro += _validator.PasswordErro != null ? $"\n- {_validator.PasswordErro}" : "";

            await DisplayAlert("Erro", mensagemErro, "OK");
        }       
    }

    private async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage(_apiService, _validator));
    }
}