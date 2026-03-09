using SQLite;
using IscaeAnnuaire.Models;

namespace IscaeAnnuaire.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitAsync()
        {
            if (_database != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "iscae.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Departement>();
            await _database.CreateTableAsync<Filiere>();
            await _database.CreateTableAsync<Enseignant>();
            await _database.CreateTableAsync<Etudiant>();

            await SeedDataAsync();
        }

        // ===================== SEED DATA =====================
        private async Task SeedDataAsync()
        {
            var deps = await _database.Table<Departement>().ToListAsync();
            if (deps.Count > 0) return;

            // Départements
            var med = new Departement { Nom = "MED", Description = "Management, Economie et Droit" };
            var mqi = new Departement { Nom = "MQI", Description = "Méthodes Quantitatives et Informatiques" };
            await _database.InsertAsync(med);
            await _database.InsertAsync(mqi);

            // Filières MED
            await _database.InsertAsync(new Filiere { Nom = "Banques & Assurances", Niveau = "Licence", DepartementId = med.Id });
            await _database.InsertAsync(new Filiere { Nom = "Finance & Comptabilité", Niveau = "Licence", DepartementId = med.Id });
            await _database.InsertAsync(new Filiere { Nom = "Gestion des Ressources Humaines", Niveau = "Licence", DepartementId = med.Id });
            await _database.InsertAsync(new Filiere { Nom = "Techniques Commerciales et Marketing", Niveau = "Licence", DepartementId = med.Id });
            await _database.InsertAsync(new Filiere { Nom = "Master Pro. Finance et Comptabilité", Niveau = "Master", DepartementId = med.Id });

            // Filières MQI
            await _database.InsertAsync(new Filiere { Nom = "Développement Informatique", Niveau = "Licence", DepartementId = mqi.Id });
            await _database.InsertAsync(new Filiere { Nom = "Informatique de Gestion", Niveau = "Licence", DepartementId = mqi.Id });
            await _database.InsertAsync(new Filiere { Nom = "Réseaux Informatiques et Télécommunications", Niveau = "Licence", DepartementId = mqi.Id });
            await _database.InsertAsync(new Filiere { Nom = "Statistique Appliquée à l'Economie", Niveau = "Licence", DepartementId = mqi.Id });
            await _database.InsertAsync(new Filiere { Nom = "Master Pro. Informatique Appliquée à la Gestion", Niveau = "Master", DepartementId = mqi.Id });
        }

        // ===================== DEPARTEMENTS =====================
        public async Task<List<Departement>> GetDepartementsAsync() =>
            await _database.Table<Departement>().ToListAsync();

        // ===================== FILIERES =====================
        public async Task<List<Filiere>> GetFilieresAsync() =>
            await _database.Table<Filiere>().ToListAsync();

        public async Task<List<Filiere>> GetFilieresByDeptAsync(int deptId) =>
            await _database.Table<Filiere>().Where(f => f.DepartementId == deptId).ToListAsync();

        // ===================== ENSEIGNANTS =====================
        public async Task<List<Enseignant>> GetEnseignantsAsync() =>
            await _database.Table<Enseignant>().ToListAsync();

        public async Task<int> SaveEnseignantAsync(Enseignant e) =>
            e.Id == 0 ? await _database.InsertAsync(e) : await _database.UpdateAsync(e);

        public async Task<int> DeleteEnseignantAsync(Enseignant e) =>
            await _database.DeleteAsync(e);

        // ===================== ETUDIANTS =====================
        public async Task<List<Etudiant>> GetEtudiantsAsync() =>
            await _database.Table<Etudiant>().ToListAsync();

        public async Task<int> SaveEtudiantAsync(Etudiant e) =>
            e.Id == 0 ? await _database.InsertAsync(e) : await _database.UpdateAsync(e);

        public async Task<int> DeleteEtudiantAsync(Etudiant e) =>
            await _database.DeleteAsync(e);
    }
}
