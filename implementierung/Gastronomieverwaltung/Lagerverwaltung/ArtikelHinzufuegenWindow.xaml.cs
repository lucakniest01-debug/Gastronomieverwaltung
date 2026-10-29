using System.IO;
using System.Windows;

namespace Lagerverwaltung
{
    public partial class ArtikelHinzufuegenWindow : Window
    {
        public ArtikelHinzufuegenWindow()
        {
            InitializeComponent();
            txtName.Focus();
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name == "" || name.Contains('|'))
            {
                MessageBox.Show("Bitte einen gültigen Artikelnamen eingeben.");
                return;
            }
            if (!int.TryParse(txtMenge.Text, out int menge) || menge <= 0)
            {
                MessageBox.Show("Bitte eine gültige Menge (größer als 0) eingeben.");
                return;
            }

            List<Lagerartikel> liste = LagerDatei.Laden();
            Lagerartikel? vorhanden = liste.FirstOrDefault(a =>
                a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (vorhanden != null)
            {
                vorhanden.Bestand += menge;
            }
            else
            {
                if (!int.TryParse(txtMinimum.Text, out int minimum) || minimum < 0)
                {
                    MessageBox.Show("Bitte für den neuen Artikel einen gültigen Mindestbestand eingeben.");
                    return;
                }
                liste.Add(new Lagerartikel { Name = name, Bestand = menge, Mindestbestand = minimum });
            }

            try
            {
                LagerDatei.Speichern(liste);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Der Lagerbestand konnte nicht gespeichert werden.\n\n" + ex.Message);
                return;
            }

            DialogResult = true;
        }
    }
}
