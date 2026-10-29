using System.IO;
using System.Text;

namespace Lagerverwaltung
{
    public static class LagerDatei
    {
        public static string Pfad => Path.Combine(AppContext.BaseDirectory, "Lagerbestand.txt");

        public static List<Lagerartikel> Laden()
        {
            List<Lagerartikel> liste = new List<Lagerartikel>();
            if (!File.Exists(Pfad))
                return liste;

            foreach (string zeile in File.ReadAllLines(Pfad))
            {
                string[] teile = zeile.Split('|');
                if (teile.Length < 3
                    || !int.TryParse(teile[1].Trim(), out int bestand)
                    || !int.TryParse(teile[2].Trim(), out int minimum))
                    continue;

                liste.Add(new Lagerartikel
                {
                    Name = teile[0].Trim(),
                    Bestand = bestand,
                    Mindestbestand = minimum
                });
            }
            return liste;
        }

        public static void Speichern(List<Lagerartikel> liste)
        {
            string linie = new string('=', 52);
            StringBuilder text = new StringBuilder();
            text.AppendLine(linie);
            text.AppendLine("  LAGERBESTAND  –  Schwimmbad-Kiosk");
            text.AppendLine($"  Stand: {DateTime.Now:dd.MM.yyyy}");
            text.AppendLine(linie);
            text.AppendLine($"  {"Artikel",-18}| {"Bestand",8} | {"Mindestbestand",14}");
            text.AppendLine(new string('-', 52));
            foreach (Lagerartikel a in liste)
                text.AppendLine($"  {a.Name,-18}| {a.Bestand,8} | {a.Mindestbestand,14}");
            text.AppendLine(linie);
            text.AppendLine("  Neue Artikel einfach als Zeile im gleichen Format");
            text.AppendLine("  ergänzen:  Name | Bestand | Mindestbestand");
            text.AppendLine(linie);

            File.WriteAllText(Pfad, text.ToString(), new UTF8Encoding(true));
        }
    }
}
