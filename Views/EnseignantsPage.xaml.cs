using IscaeAnnuaire.Data;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Views
{
    public partial class EnseignantsPage : ContentPage
    {
        private readonly DatabaseService _db;
        private List<EnseignantDisplay> _tousEnseignants = new();

        public EnseignantsPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "+",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(
                        new FormulaireEnseignantPage(new EnseignantDisplay(), _db));
                })
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ChargerEnseignants();
        }

        private async Task ChargerEnseignants()
        {
            await _db.InitAsync();
            var enseignants = await _db.GetEnseignantsAsync();
            var departements = await _db.GetDepartementsAsync();

            _tousEnseignants = enseignants.Select(e => new EnseignantDisplay
            {
                Id = e.Id,
                Nom = e.Nom,
                Prenom = e.Prenom,
                Grade = e.Grade,
                Telephone = e.Telephone,
                Email = e.Email,
                DepartementId = e.DepartementId,
                DepartementNom = departements.FirstOrDefault(d => d.Id == e.DepartementId)?.Nom ?? ""
            }).ToList();

            AppliquerFiltres();
        }

        private void AppliquerFiltres()
        {
            var resultat = _tousEnseignants.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchBar.Text))
            {
                var search = SearchBar.Text.ToLower();
                resultat = resultat.Where(e =>
                    e.Nom.ToLower().Contains(search) ||
                    e.Prenom.ToLower().Contains(search));
            }

            if (PickerDept.SelectedIndex > 0)
                resultat = resultat.Where(e => e.DepartementNom == PickerDept.SelectedItem.ToString());

            if (PickerGrade.SelectedIndex > 0)
                resultat = resultat.Where(e => e.Grade == PickerGrade.SelectedItem.ToString());

            var liste = resultat.ToList();
            EnseignantsList.ItemsSource = liste;
            LblCount.Text = $"{liste.Count} enseignant(s) trouvé(s)";
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e) => AppliquerFiltres();
        private void OnFiltreChanged(object sender, EventArgs e) => AppliquerFiltres();

        private async void OnEnseignantTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is EnseignantDisplay enseignant)
            {
                EnseignantsList.SelectedItem = null;
                await Navigation.PushAsync(new DetailEnseignantPage(enseignant, _db));
            }
        }
    }

    public class EnseignantDisplay : Enseignant
    {
        public string DepartementNom { get; set; }
    }
}
