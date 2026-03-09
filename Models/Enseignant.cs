using SQLite;

namespace IscaeAnnuaire.Models
{
    public class Enseignant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Grade { get; set; }
        public int DepartementId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
