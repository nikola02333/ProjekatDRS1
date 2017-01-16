using Biblioteka.Access;
using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for IzmenaPodataka.xaml
    /// </summary>
    public partial class IzmenaPodataka : Window
    {
        ChannelFactory<ICompanyDB> factory;
        Osoba zaposlen;
        Osoba izmenjeni;
        int mojiPodaci;

        public IzmenaPodataka(Osoba zap, ChannelFactory<ICompanyDB> fact, int broj)
        {
            InitializeComponent();

            this.factory = fact;
            this.zaposlen = zap;
            this.mojiPodaci = broj;

            PopunjavanjeUlogaCB();
            PopunjavanjePodataka();
        }

        private void PopunjavanjeUlogaCB()
        {
            ulogaCB.Items.Add("radnik");
            ulogaCB.Items.Add("direktor");
            ulogaCB.Items.Add("hr");
        }

        private void PopunjavanjePodataka()
        {
            if (mojiPodaci == 1)
            {
                poruka.Visibility = Visibility.Hidden;

                korImeTB.IsEnabled = true;
                lozinkaTB.IsEnabled = true;
                prvTB.IsEnabled = true;
                krvTB.IsEnabled = true;
                emailTB.IsEnabled = true;
            }
            else if(mojiPodaci == 2)
            {
                poruka.Visibility = Visibility.Visible;
                poruka.Content = "Isteklo je 6 meseci - promenite lozinku!";
                poruka.Foreground = Brushes.Red;

                korImeTB.IsEnabled = false;
                lozinkaTB.IsEnabled = true;
                ulogaCB.IsEnabled = false;
                prvTB.IsEnabled = false;
                krvTB.IsEnabled = false;
                emailTB.IsEnabled = false;
            }
            else
            {
                poruka.Visibility = Visibility.Hidden;

                korImeTB.IsEnabled = false;
                lozinkaTB.IsEnabled = false;
                prvTB.IsEnabled = false;
                krvTB.IsEnabled = false;
                emailTB.IsEnabled = false;
            }

            korImeTB.Text = zaposlen.KorIme;
            lozinkaTB.Text = zaposlen.Lozinka;
            ulogaCB.Text = zaposlen.Uloga;
            prvTB.Text = zaposlen.RadnoVremeStart;
            krvTB.Text = zaposlen.RadnoVremeKraj;
            emailTB.Text = zaposlen.Email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            izmenjeni = new Osoba();
            izmenjeni.KorIme = korImeTB.Text.Trim();
            izmenjeni.Lozinka = lozinkaTB.Text.Trim();
            izmenjeni.Uloga = (string)ulogaCB.SelectedItem;
            izmenjeni.RadnoVremeStart = prvTB.Text.Trim();
            izmenjeni.RadnoVremeKraj = krvTB.Text.Trim();
            izmenjeni.Prijavljen = zaposlen.Prijavljen;
            izmenjeni.Email = zaposlen.Email.Trim();

            if(!zaposlen.Lozinka.Equals(izmenjeni.Lozinka))
            {
                izmenjeni.VremeLozinke = DateTime.Now;
            }
            else
            {
                izmenjeni.VremeLozinke = zaposlen.VremeLozinke;
            }

            ICompanyDB proxy = factory.CreateChannel();
            proxy.ReplaceAction(zaposlen, izmenjeni);

            this.Close();
        }
    }
}
