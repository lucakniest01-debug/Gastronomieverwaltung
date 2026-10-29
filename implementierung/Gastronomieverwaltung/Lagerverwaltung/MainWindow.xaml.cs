using System.IO;
using System.Text;
using System.Windows;

namespace Lagerverwaltung
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BestandLaden();
        }

        private void BestandLaden()
        {
            if (!File.Exists(LagerDatei.Pfad))
            {
                txtBestand.Text = "Die Datei Lagerbestand.txt wurde nicht gefunden.";
                return;
            }

            try
            {
                StringBuilder text = new StringBuilder();
                text.AppendLine($"{"Artikel",-15}{"Bestand",8}{"Minimum",9}");
                text.AppendLine(new string('-', 32));

                foreach (Lagerartikel a in LagerDatei.Laden())
                {
                    string warnung = a.Bestand < a.Mindestbestand ? "  ! nachbestellen" : "";
                    text.AppendLine($"{a.Name,-15}{a.Bestand,8}{a.Mindestbestand,9}{warnung}");
                }

                txtBestand.Text = text.ToString();
            }
            catch (IOException ex)
            {
                txtBestand.Text = "Fehler beim Lesen der Datei:\n" + ex.Message;
            }
        }

        private void BtnArtikelHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            ArtikelHinzufuegenWindow fenster = new ArtikelHinzufuegenWindow { Owner = this };
            if (fenster.ShowDialog() == true)
                BestandLaden();
        }

        private void BtnArtikelBestellen_Click(object sender, RoutedEventArgs e)
        {
            ArtikelBestellenWindow fenster = new ArtikelBestellenWindow { Owner = this };
            fenster.ShowDialog();
        }
    }
}
