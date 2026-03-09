namespace IscaeAnnuaire.Views
{
    public partial class LoginPage : ContentPage
    {
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "iscae2025";

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text?.Trim() ?? "";
            string password = EntryPassword.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LblErreur.Text = "Veuillez remplir tous les champs";
                LblErreur.IsVisible = true;
                return;
            }

            if (username == ADMIN_USERNAME && password == ADMIN_PASSWORD)
            {
                LblErreur.IsVisible = false;
                Application.Current.MainPage = App.CreateMainPage();
            }
            else
            {
                LblErreur.Text = "Nom d'utilisateur ou mot de passe incorrect";
                LblErreur.IsVisible = true;
                EntryPassword.Text = "";
            }
        }
    }
}
