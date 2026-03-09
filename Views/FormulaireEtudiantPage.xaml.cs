using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class FormulaireEtudiantPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly EtudiantDisplay _etudiant;
        private List<Filiere> _filieres = new();

        public FormulaireEtudiantPage(EtudiantDisplay etudiant, DatabaseService db)
        {
            InitializeComponent();
            _db = db;
            _etudiant = etudiant;
            Title = etudiant.Id != 0 ? "Modifier Étudiant" : "Ajouter Étudiant";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _db.InitAsync();
            _filieres = await _db.GetFilieresAsync();
            PickerFiliere.ItemsSource = _filieres.Select(f => f.Nom).ToList();

            if (_etudiant.Id != 0)
                ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            EntryNom.Text = _etudiant.Nom;
            EntryPrenom.Text = _etudiant.Prenom;
            EntryCNI.Text = _etudiant.CNI;
            EntryTelephone.Text = _etudiant.Telephone;
            EntryEmail.Text = _etudiant.Email;
            EntryAnnee.Text = _etudiant.AnneeAcademique;

            var filiereIndex = _filieres.FindIndex(f => f.Id == _etudiant.FiliereId);
            PickerFiliere.SelectedIndex = filiereIndex;

            var niveaux = new List<string> { "L1", "L2", "L3", "M1", "M2" };
            PickerNiveau.SelectedIndex = niveaux.IndexOf(_etudiant.Niveau ?? "");

            var statuts = new List<string> { "Actif", "Inactif" };
            PickerStatut.SelectedIndex = statuts.IndexOf(_etudiant.Statut ?? "Actif");
        }

        private void OnFiliereChanged(object sender, EventArgs e) { }

        private async void OnEnregistrerClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNom.Text) ||
                string.IsNullOrWhiteSpace(EntryPrenom.Text))
            {
                await DisplayAlert("Erreur", "Nom et Prénom sont obligatoires", "OK");
                return;
            }

            if (PickerFiliere.SelectedIndex < 0)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une filière", "OK");
                return;
            }

            try
            {
                var filiere = _filieres[PickerFiliere.SelectedIndex];

                var etudiant = new Etudiant
                {
                    Id = _etudiant.Id,
                    Nom = EntryNom.Text.Trim(),
                    Prenom = EntryPrenom.Text.Trim(),
                    CNI = EntryCNI.Text?.Trim() ?? "",
                    Telephone = EntryTelephone.Text?.Trim() ?? "",
                    Email = EntryEmail.Text?.Trim() ?? "",
                    FiliereId = filiere.Id,
                    Niveau = PickerNiveau.SelectedItem?.ToString() ?? "",
                    AnneeAcademique = EntryAnnee.Text?.Trim() ?? "",
                    Statut = PickerStatut.SelectedItem?.ToString() ?? "Actif"
                };

                await _db.SaveEtudiantAsync(etudiant);
                await DisplayAlert("Succès", "Étudiant enregistré avec succès", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Problème : {ex.Message}", "OK");
            }
        }
    }
}
