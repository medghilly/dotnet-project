using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class DetailEtudiantPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly EtudiantDisplay _etudiant;

        public DetailEtudiantPage(EtudiantDisplay etudiant, DatabaseService db)
        {
            InitializeComponent();
            _db = db;
            _etudiant = etudiant;
            ChargerDetails();
        }

        private void ChargerDetails()
        {
            LblAvatar.Text = _etudiant.Prenom?.Substring(0, 1).ToUpper();
            LblNomComplet.Text = $"{_etudiant.Prenom} {_etudiant.Nom}";
            LblFiliere.Text = _etudiant.FiliereNom;
            LblNiveau.Text = _etudiant.Niveau;
            LblCNI.Text = string.IsNullOrEmpty(_etudiant.CNI) ? "Non renseigné" : _etudiant.CNI;
            LblTelephone.Text = string.IsNullOrEmpty(_etudiant.Telephone) ? "Non renseigné" : _etudiant.Telephone;
            LblEmail.Text = string.IsNullOrEmpty(_etudiant.Email) ? "Non renseigné" : _etudiant.Email;
            LblAnnee.Text = _etudiant.AnneeAcademique ?? "Non renseigné";

            if (_etudiant.Statut == "Actif")
            {
                FrameStatut.BackgroundColor = Color.FromArgb("#E8F8F0");
                LblStatut.Text = "Étudiant Actif";
                LblStatut.TextColor = Color.FromArgb("#2ECC71");
            }
            else
            {
                FrameStatut.BackgroundColor = Color.FromArgb("#FDECEA");
                LblStatut.Text = "Étudiant Inactif";
                LblStatut.TextColor = Color.FromArgb("#E74C3C");
            }
        }

        private async void OnModifierClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormulaireEtudiantPage(_etudiant, _db));
        }

        private async void OnSupprimerClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmation",
                $"Supprimer {_etudiant.Prenom} {_etudiant.Nom} ?", "Oui", "Non");
            if (confirm)
            {
                try
                {
                    var etudiant = new Etudiant { Id = _etudiant.Id };
                    await _db.DeleteEtudiantAsync(etudiant);
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
