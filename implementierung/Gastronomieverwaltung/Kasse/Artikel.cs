namespace Kasse
{
    // Ein Artikel aus der Tabelle "Artikel"
    public class Artikel
    {
        public int ArtikelID { get; set; }
        public string Name { get; set; } = "";
        public decimal Preis { get; set; }
        public int Bestand { get; set; }
        public int Mindestbestand { get; set; }
        public int KategorieID { get; set; }
    }
}
