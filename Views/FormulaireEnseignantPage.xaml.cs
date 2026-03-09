using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class FormulaireEnseignantPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly EnseignantDisplay _enseignant;
        private List<Departement> _departements = new();

        public FormulaireEnseignantPage(EnseignantDisplay enseignant, DatabaseService db)
        {
            InitializeComponent();
            _db = db;
            _enseignant = enseignant;
            Title = enseignant.Id != 0 ? "Modifier Enseignant" : "Ajouter Enseignant";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _db.InitAsync();
            _departements = await _db.GetDepartementsAsync();

            if (_enseignant.Id != 0)
                ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            EntryNom.Text = _enseignant.Nom;
            EntryPrenom.Text = _enseignant.Prenom;
            EntryTelephone.Text = _enseignant.Telephone;
            EntryEmail.Text = _enseignant.Email;

            var grades = new List<string>
            {
                "Maître Assistant", "Maître de Conférences",
                "Professeur Habilité", "Technologue"
            };
            PickerGrade.SelectedIndex = grades.IndexOf(_enseignant.Grade ?? "");

            var depts = new List<string> { "MED", "MQI" };
            PickerDept.SelectedIndex = depts.IndexOf(_enseignant.DepartementNom ?? "");
        }

        private async void OnEnregistrerClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNom.Text) ||
                string.IsNullOrWhiteSpace(EntryPrenom.Text))
            {
                await DisplayAlert("Erreur", "Nom et Prénom sont obligatoires", "OK");
                return;
            }

            if (PickerDept.SelectedIndex < 0)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner un département", "OK");
                return;
            }

            try
            {
                var deptNom = PickerDept.SelectedItem.ToString();
                var dept = _departements.FirstOrDefault(d => d.Nom == deptNom);

                var enseignant = new Enseignant
                {
                    Id = _enseignant.Id,
                    Nom = EntryNom.Text.Trim(),
                    Prenom = EntryPrenom.Text.Trim(),
                    Grade = PickerGrade.SelectedItem?.ToString() ?? "",
                    DepartementId = dept?.Id ?? 0,
                    Telephone = EntryTelephone.Text?.Trim() ?? "",
                    Email = EntryEmail.Text?.Trim() ?? ""
                };

                await _db.SaveEnseignantAsync(enseignant);
                await DisplayAlert("Succès", "Enseignant enregistré avec succès", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Problème : {ex.Message}", "OK");
            }
        }
    }
}
