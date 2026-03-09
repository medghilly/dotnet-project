using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class DetailEnseignantPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly EnseignantDisplay _enseignant;

        public DetailEnseignantPage(EnseignantDisplay enseignant, DatabaseService db)
        {
            InitializeComponent();
            _db = db;
            _enseignant = enseignant;
            ChargerDetails();
        }

        private void ChargerDetails()
        {
            LblAvatar.Text = _enseignant.Prenom?.Substring(0, 1).ToUpper();
            LblNomComplet.Text = $"{_enseignant.Prenom} {_enseignant.Nom}";
            LblGrade.Text = _enseignant.Grade;
            LblDept.Text = _enseignant.DepartementNom;
            LblTelephone.Text = string.IsNullOrEmpty(_enseignant.Telephone) ? "Non renseigné" : _enseignant.Telephone;
            LblEmail.Text = string.IsNullOrEmpty(_enseignant.Email) ? "Non renseigné" : _enseignant.Email;
        }

        private async void OnModifierClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormulaireEnseignantPage(_enseignant, _db));
        }

        private async void OnSupprimerClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmation",
                $"Supprimer {_enseignant.Prenom} {_enseignant.Nom} ?", "Oui", "Non");
            if (confirm)
            {
                try
                {
                    var enseignant = new Enseignant { Id = _enseignant.Id };
                    await _db.DeleteEnseignantAsync(enseignant);
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Problème : {ex.Message}", "OK");
                }
            }
        }
    }
}
