using SQLite;

namespace IscaeAnnuaire.Models
{
    public class Filiere
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Niveau { get; set; } // Licence ou Master
        public int DepartementId { get; set; }
    }
}
