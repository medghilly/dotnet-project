using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class EtudiantsPage : ContentPage
    {
        private readonly DatabaseService _db;
        private List<EtudiantDisplay> _tousEtudiants = new();

        public EtudiantsPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ChargerEtudiants();
        }

        private async Task ChargerEtudiants()
        {
            await _db.InitAsync();
            var etudiants = await _db.GetEtudiantsAsync();
            var filieres = await _db.GetFilieresAsync();
            var departements = await _db.GetDepartementsAsync();

            _tousEtudiants = etudiants.Select(e =>
            {
                var filiere = filieres.FirstOrDefault(f => f.Id == e.FiliereId);
                var dept = departements.FirstOrDefault(d => d.Id == filiere?.DepartementId);
                return new EtudiantDisplay
                {
                    Id = e.Id,
                    Nom = e.Nom,
                    Prenom = e.Prenom,
                    CNI = e.CNI,
                    Telephone = e.Telephone,
                    Email = e.Email,
                    FiliereId = e.FiliereId,
                    Niveau = e.Niveau,
                    AnneeAcademique = e.AnneeAcademique,
                    Statut = e.Statut,
                    FiliereNom = filiere?.Nom ?? "",
                    DepartementNom = dept?.Nom ?? "",
                    StatutColor = e.Statut == "Actif" ? "#2ECC71" : "#E74C3C"
                };
            }).ToList();

            AppliquerFiltres();
        }

        private void AppliquerFiltres()
        {
            var resultat = _tousEtudiants.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchBar.Text))
            {
                var search = SearchBar.Text.ToLower();
                resultat = resultat.Where(e =>
                    e.Nom.ToLower().Contains(search) ||
                    e.Prenom.ToLower().Contains(search) ||
                    (e.CNI != null && e.CNI.ToLower().Contains(search)));
            }

            if (PickerDept.SelectedIndex > 0)
                resultat = resultat.Where(e => e.DepartementNom == PickerDept.SelectedItem.ToString());

            if (PickerNiveau.SelectedIndex > 0)
                resultat = resultat.Where(e => e.Niveau == PickerNiveau.SelectedItem.ToString());

            if (PickerStatut.SelectedIndex > 0)
                resultat = resultat.Where(e => e.Statut == PickerStatut.SelectedItem.ToString());

            var liste = resultat.ToList();
            EtudiantsList.ItemsSource = liste;
            LblCount.Text = $"{liste.Count} étudiant(s) trouvé(s)";
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e) => AppliquerFiltres();
        private void OnFiltreChanged(object sender, EventArgs e) => AppliquerFiltres();

        private async void OnEtudiantTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is EtudiantDisplay etudiant)
            {
                EtudiantsList.SelectedItem = null;
                await Navigation.PushAsync(new DetailEtudiantPage(etudiant, _db));
            }
        }

        private async void OnAjouterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormulaireEtudiantPage(new EtudiantDisplay(), _db));
        }
    }

    public class EtudiantDisplay : Etudiant
    {
        public string FiliereNom { get; set; }
        public string DepartementNom { get; set; }
        public string StatutColor { get; set; }
    }
}
