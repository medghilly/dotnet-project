using IscaeAnnuaire.Views;

namespace IscaeAnnuaire
{
    public partial class App : Application
    {
        private static AccueilPage _accueilPage;
        private static DepartementsPage _departementsPage;
        private static EnseignantsPage _enseignantsPage;
        private static EtudiantsPage _etudiantsPage;

        public App(AccueilPage accueilPage, DepartementsPage departementsPage,
                   EnseignantsPage enseignantsPage, EtudiantsPage etudiantsPage)
        {
            InitializeComponent();
            _accueilPage = accueilPage;
            _departementsPage = departementsPage;
            _enseignantsPage = enseignantsPage;
            _etudiantsPage = etudiantsPage;

            // Démarrer avec la page Login
            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                BarTextColor = Colors.White
            };
        }

        public static TabbedPage CreateMainPage()
        {
            return new TabbedPage
            {
                BackgroundColor = Color.FromArgb("#F5F5F5"),
                BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                BarTextColor = Colors.White,
                SelectedTabColor = Color.FromArgb("#2ECC71"),
                UnselectedTabColor = Colors.White,
                Children =
                {
                    new NavigationPage(_accueilPage)
                    {
                        Title = "Accueil",
                        BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                        BarTextColor = Colors.White
                    },
                    new NavigationPage(_departementsPage)
                    {
                        Title = "Départements",
                        BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                        BarTextColor = Colors.White
                    },
                    new NavigationPage(_enseignantsPage)
                    {
                        Title = "Enseignants",
                        BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                        BarTextColor = Colors.White
                    },
                    new NavigationPage(_etudiantsPage)
                    {
                        Title = "Étudiants",
                        BarBackgroundColor = Color.FromArgb("#1B2A6B"),
                        BarTextColor = Colors.White
                    }
                }
            };
        }
    }
}
