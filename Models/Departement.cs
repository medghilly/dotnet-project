using SQLite;

namespace IscaeAnnuaire.Models
{
    public class Departement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
    }
}
