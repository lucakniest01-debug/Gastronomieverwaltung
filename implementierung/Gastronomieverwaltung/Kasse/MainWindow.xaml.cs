using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Kasse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Verbindung =
            @"Server=(localdb)\MSSQLLocalDB;Database=Gastronomieverwaltung;" +
            "Integrated Security=True;TrustServerCertificate=True";

        // Alle Artikel aus der Datenbank
        private List<Artikel> artikelListe = new List<Artikel>();

        public MainWindow()
        {
            InitializeComponent();
            ArtikelLaden();
            cmbArtikel.ItemsSource = artikelListe;
        }

        // Lädt beim Start alle Artikel aus der Datenbank in die Liste
        private void ArtikelLaden()
        {
            try
            {
                using SqlConnection con = new SqlConnection(Verbindung);
                con.Open();

                string sql = "SELECT Artikel_ID, Name, Preis, Bestand, Mindestbestand, Kategorie_ID FROM Artikel";
                using SqlCommand cmd = new SqlCommand(sql, con);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    artikelListe.Add(new Artikel
                    {
                        ArtikelID      = reader.GetInt32(0),
                        Name           = reader.GetString(1),
                        Preis          = reader.GetDecimal(2),
                        Bestand        = reader.GetInt32(3),
                        Mindestbestand = reader.GetInt32(4),
                        KategorieID    = reader.GetInt32(5)
                    });
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Artikel konnten nicht geladen werden.\n\n" + ex.Message);
            }
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

        // Addiert die Summen aller Bestellpositionen und zeigt sie unten rechts an
        private void GesamtpreisAktualisieren()
        {
            decimal gesamt = lstBestellung.Items.OfType<Bestellposition>().Sum(p => p.Summe);
            txtGesamt.Text = "Gesamt: " + gesamt.ToString("C", new CultureInfo("de-DE"));
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
