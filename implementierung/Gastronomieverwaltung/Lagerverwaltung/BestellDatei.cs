using System.IO;
using System.Text;

namespace Lagerverwaltung
{
    public static class BestellDatei
    {
        public static string Pfad => Path.Combine(AppContext.BaseDirectory, "Bestellungen.txt");

        public static int NaechsteNummer()
        {
            if (!File.Exists(Pfad))
                return 1;

            return File.ReadAllLines(Pfad).Count(z => z.StartsWith("Bestellung Nr.")) + 1;
        }

        public static int Speichern(List<Bestellposition> positionen)
        {
            int nummer = NaechsteNummer();
            string linie = new string('=', 44);

            StringBuilder text = new StringBuilder();
            text.AppendLine(linie);
            text.AppendLine($"Bestellung Nr. {nummer}");
            text.AppendLine($"Datum: {DateTime.Now:dd.MM.yyyy HH:mm}");
            text.AppendLine(new string('-', 44));
            text.AppendLine($"{"Artikel",-20}{"Menge",8}{"Bestand",9}");
            foreach (Bestellposition p in positionen)
                text.AppendLine($"{p.Artikel.Name,-20}{p.Menge,8}{p.Artikel.Bestand,9}");
            text.AppendLine(linie);
            text.AppendLine();

            File.AppendAllText(Pfad, text.ToString(), new UTF8Encoding(true));
            return nummer;
        }
    }
}
