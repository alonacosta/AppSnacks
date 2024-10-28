using AppSnacks.Pages;
using AppSnacks.Services;
using AppSnacks.Validations;

namespace AppSnacks
{
    public partial class AppShell : Shell
    {
        private readonly ApiService _apiService;
        private readonly IValidator _validator;

        public AppShell(ApiService apiService, IValidator validator)
        {
            InitializeComponent();
            _apiService = apiService;
            _validator = validator;

            ConfigureShell();
        }

        private void ConfigureShell()
        {
            var homePage = new HomePage(_apiService, _validator);
            var cartPage = new CartPage(_apiService, _validator);
            var favoritePage = new FavouritePage(_apiService, _validator);
            var profilePage = new ProfilePage(_apiService, _validator);

            Items.Add(new TabBar
            {
                Items =
            {
                new ShellContent { Title = "Home",Icon = "home",Content = homePage  },
                new ShellContent { Title = "Carrinho", Icon = "cart",Content = cartPage },
                new ShellContent { Title = "Favoritos",Icon = "heart",Content = favoritePage },
                new ShellContent { Title = "Perfil",Icon = "profile",Content = profilePage }
            }
            });
        }

    }
}
