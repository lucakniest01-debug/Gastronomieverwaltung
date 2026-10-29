using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;

namespace KasseOhneDB
{
    /// <summary>
    /// Kasse ohne Datenbank: Die Artikel stehen fest im Programm.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Alle Artikel (gleiche Daten wie in der Datenbank)
        private List<Artikel> artikelListe = new List<Artikel>();

        public MainWindow()
        {
            InitializeComponent();
            ArtikelLaden();
            cmbArtikel.ItemsSource = artikelListe;
        }

        // Füllt die Liste mit festen Artikeln statt aus der Datenbank
        private void ArtikelLaden()
        {
            artikelListe.Add(new Artikel { ArtikelID = 1, Name = "Limo",         Preis = 1.40m, Bestand = 20, Mindestbestand = 12, KategorieID = 1 });
            artikelListe.Add(new Artikel { ArtikelID = 2, Name = "Kaffee",       Preis = 1.20m, Bestand = 14, Mindestbestand = 8,  KategorieID = 2 });
            artikelListe.Add(new Artikel { ArtikelID = 3, Name = "Pommes",       Preis = 2.20m, Bestand = 22, Mindestbestand = 10, KategorieID = 3 });
            artikelListe.Add(new Artikel { ArtikelID = 4, Name = "Gummibärchen", Preis = 0.99m, Bestand = 9,  Mindestbestand = 4,  KategorieID = 4 });
            artikelListe.Add(new Artikel { ArtikelID = 5, Name = "Schokolade",   Preis = 1.00m, Bestand = 5,  Mindestbestand = 2,  KategorieID = 4 });
        }

        // Sucht einen Artikel in der Liste per ID oder Name
        private Artikel? ArtikelSuchen(string eingabe)
        {
            if (int.TryParse(eingabe, out int id))
                return artikelListe.FirstOrDefault(a => a.ArtikelID == id);

            return artikelListe.FirstOrDefault(a =>
                a.Name.Equals(eingabe, StringComparison.OrdinalIgnoreCase));
        }

        private void BtnHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            // Ausgewählter Artikel aus der Liste – oder getippter Name/ID
            Artikel? artikel = cmbArtikel.SelectedItem as Artikel
                               ?? ArtikelSuchen(cmbArtikel.Text.Trim());

            if (artikel == null)
            {
                MessageBox.Show("Bitte einen Artikel aus der Liste auswählen.");
                return;
            }
            if (!int.TryParse(txtAnzahl.Text, out int anzahl) || anzahl <= 0)
            {
                MessageBox.Show("Bitte eine gültige Anzahl (größer als 0) eingeben.");
                return;
            }

            lstBestellung.Items.Add(new Bestellposition { Artikel = artikel, Anzahl = anzahl });
            GesamtpreisAktualisieren();
            cmbArtikel.SelectedIndex = -1;
            cmbArtikel.Text = "";
            txtAnzahl.Clear();
            cmbArtikel.Focus();
        }

        // Leert die Bestellung und setzt den Gesamtpreis zurück
        private void BtnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            lstBestellung.Items.Clear();
            GesamtpreisAktualisieren();
        }

        // Addiert die Summen aller Bestellpositionen und zeigt sie unten rechts an
        private void GesamtpreisAktualisieren()
        {
            decimal gesamt = lstBestellung.Items.OfType<Bestellposition>().Sum(p => p.Summe);
            txtGesamt.Text = "Gesamt: " + gesamt.ToString("C", new CultureInfo("de-DE"));
        }

        // Speicherort der Bestelldatei: Dokumente\Kasse_Bestellungen.txt
        private readonly string bestellDatei = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Kasse_Bestellungen.txt");

        // Schließt die Bestellung ab und hängt sie an die Bestelldatei an
        private void BtnAbschliessen_Click(object sender, RoutedEventArgs e)
        {
            List<Bestellposition> positionen = lstBestellung.Items.OfType<Bestellposition>().ToList();
            if (positionen.Count == 0)
            {
                MessageBox.Show("Die Bestellung ist leer.");
                return;
            }

            CultureInfo de = new CultureInfo("de-DE");
            int nummer = NaechsteBestellnummer();
            decimal gesamt = positionen.Sum(p => p.Summe);

            // Bestellung als Text aufbauen
            StringBuilder text = new StringBuilder();
            text.AppendLine("==============================================");
            text.AppendLine($"Bestellung Nr. {nummer}");
            text.AppendLine($"Datum: {DateTime.Now.ToString("dd.MM.yyyy HH:mm", de)}");
            text.AppendLine("----------------------------------------------");
            foreach (Bestellposition p in positionen)
            {
                text.AppendLine($"{p.Text,-30}{p.Summe.ToString("C", de),15}");
            }
            text.AppendLine("----------------------------------------------");
            text.AppendLine($"{"Gesamt:",-30}{gesamt.ToString("C", de),15}");
            text.AppendLine();

            try
            {
                // AppendAllText legt die Datei beim ersten Mal an und hängt danach immer unten an
                File.AppendAllText(bestellDatei, text.ToString(), Encoding.UTF8);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Die Bestellung konnte nicht gespeichert werden.\n\n" + ex.Message);
                return;
            }

            MessageBox.Show($"Bestellung Nr. {nummer} über {gesamt.ToString("C", de)} wurde gespeichert.\n\n{bestellDatei}");

            lstBestellung.Items.Clear();
            txtAnzahl.Clear();
            GesamtpreisAktualisieren();
        }

        // Liest die Bestelldatei und zählt, wie viele Bestellungen schon drinstehen
        private int NaechsteBestellnummer()
        {
            if (!File.Exists(bestellDatei))
                return 1;

            int anzahl = File.ReadAllLines(bestellDatei)
                             .Count(zeile => zeile.StartsWith("Bestellung Nr."));
            return anzahl + 1;
        }
    }

    // Eine Zeile in der Bestellübersicht
    public class Bestellposition
    {
        public Artikel Artikel { get; set; } = new Artikel();
        public int Anzahl { get; set; }

        public decimal Summe => Anzahl * Artikel.Preis;
        public string Text => $"{Anzahl}x {Artikel.Name}";
        public string PreisText => Summe.ToString("C", new CultureInfo("de-DE"));
    }
}
