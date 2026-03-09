using SQLite;

namespace IscaeAnnuaire.Models
{
    public class Etudiant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CNI { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int FiliereId { get; set; }
        public string Niveau { get; set; } // L1, L2, L3, M1, M2
        public string AnneeAcademique { get; set; }
        public string Statut { get; set; } // Actif / Inactif
    }
}
