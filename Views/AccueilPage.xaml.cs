using IscaeAnnuaire.Data;

namespace IscaeAnnuaire.Views
{
    public partial class AccueilPage : ContentPage
    {
        private readonly DatabaseService _db;

        public AccueilPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _db.InitAsync();

            var enseignants = await _db.GetEnseignantsAsync();
            var etudiants = await _db.GetEtudiantsAsync();

            LblEnseignants.Text = enseignants.Count.ToString();
            LblEtudiants.Text = etudiants.Count.ToString();
        }
    }
}
