namespace Lagerverwaltung
{
    public class Bestellposition
    {
        public Lagerartikel Artikel { get; set; } = new Lagerartikel();
        public int Menge { get; set; }

        public override string ToString() => $"{Menge}x {Artikel.Name}";
    }
}
