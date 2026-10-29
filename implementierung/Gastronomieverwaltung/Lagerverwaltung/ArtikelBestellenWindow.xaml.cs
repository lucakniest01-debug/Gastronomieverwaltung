using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lagerverwaltung
{
    public partial class ArtikelBestellenWindow : Window
    {
        public ArtikelBestellenWindow()
        {
            InitializeComponent();
            cmbArtikel.ItemsSource = LagerDatei.Laden();
        }

        private void CmbArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArtikel.SelectedItem is not Lagerartikel artikel)
            {
                txtInfo.Text = "";
                return;
            }

            txtInfo.Text = $"Bestand: {artikel.Bestand}   Minimum: {artikel.Mindestbestand}";

            int vorschlag = artikel.Mindestbestand * 2 - artikel.Bestand;
            txtMenge.Text = vorschlag > 0 ? vorschlag.ToString() : "";
        }

        private void BtnHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArtikel.SelectedItem is not Lagerartikel artikel)
            {
                MessageBox.Show("Bitte einen Artikel auswählen.");
                return;
            }
            if (!int.TryParse(txtMenge.Text, out int menge) || menge <= 0)
            {
                MessageBox.Show("Bitte eine gültige Menge (größer als 0) eingeben.");
                return;
            }

            Bestellposition? vorhanden = lstBestellung.Items.OfType<Bestellposition>()
                .FirstOrDefault(p => p.Artikel == artikel);

            if (vorhanden != null)
            {
                vorhanden.Menge += menge;
                lstBestellung.Items.Refresh();
            }
            else
            {
                lstBestellung.Items.Add(new Bestellposition { Artikel = artikel, Menge = menge });
            }

            cmbArtikel.SelectedIndex = -1;
            txtMenge.Clear();
        }

        private void BtnEntfernen_Click(object sender, RoutedEventArgs e)
        {
            if (lstBestellung.SelectedItem != null)
                lstBestellung.Items.Remove(lstBestellung.SelectedItem);
        }

        private void BtnAbschliessen_Click(object sender, RoutedEventArgs e)
        {
            List<Bestellposition> positionen = lstBestellung.Items.OfType<Bestellposition>().ToList();
            if (positionen.Count == 0)
            {
                MessageBox.Show("Die Bestellung ist leer.");
                return;
            }

            int nummer;
            try
            {
                nummer = BestellDatei.Speichern(positionen);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Die Bestellung konnte nicht gespeichert werden.\n\n" + ex.Message);
                return;
            }

            MessageBox.Show($"Bestellung Nr. {nummer} wurde gespeichert.\n\n{BestellDatei.Pfad}");
            DialogResult = true;
        }
    }
}
