using Biblioteka.Access;
using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for SektorHR.xaml
    /// </summary>
    public partial class SektorHR : Window
    {
        ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

        Osoba z;
        Osoba trenutni;
        DateTime vremeDolaskaNaPosao;

        public SektorHR (Osoba zaposlen)
        {
            InitializeComponent();

            ICompanyDB proxy = factory.CreateChannel();
            List<Osoba> privremena = proxy.Read();

            foreach (Osoba o in privremena)
            {
                if (o.KorIme.Equals(zaposlen.KorIme))
                {
                    if (o.Lozinka.Equals(zaposlen.Lozinka))
                    {
                        this.z = o;
                        break;
                    }
                }
            }

            this.trenutni = z;
            trenutni.Prijavljen = true;
            proxy.ReplaceAction(z, trenutni);

            this.vremeDolaskaNaPosao = DateTime.Now;
            //           dolazakNaPosao();                         OTKOMENTARISATI!!!!!!!!!!!!!

            promenaLozinke();

            pregledZaposlenih();
        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            ICompanyDB proxy = factory.CreateChannel();

            trenutni.Prijavljen = false;
            proxy.ReplaceAction(z, trenutni);

            this.Close();
        }

        private void pregledZaposlenih()
        {
            sviZaposleniCB.SelectedItem = null;
            sviZaposleniCB.Items.Clear();

            ICompanyDB proxy = factory.CreateChannel();

            List<Osoba> baza = proxy.Read();

            foreach (var zaposlen in baza)
            {
                if (zaposlen.KorIme.Equals(trenutni.KorIme))
                {
                    if (zaposlen.Lozinka.Equals(trenutni.Lozinka))
                    {
                        continue;
                    }
                    else
                    {
                        sviZaposleniCB.Items.Add(zaposlen);
                        continue;
                    }
                }
                else
                {
                    sviZaposleniCB.Items.Add(zaposlen);
                }
            }
        }

        private void Button_Click_2 (object sender, RoutedEventArgs e)
        {
            NoviZaposleni novi = new NoviZaposleni(factory);
            novi.Owner = this;
            novi.ShowDialog();
        }

        private void sviZaposleniCB_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            if (!(sviZaposleniCB.SelectedItem == null))
            {
                IzmenaPodataka izmena = new IzmenaPodataka((Osoba)sviZaposleniCB.SelectedItem, factory, 0);
                izmena.Owner = this;
                izmena.ShowDialog();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IzmenaPodataka izmena = new IzmenaPodataka(trenutni, factory, 1);
            izmena.Owner = this;
            izmena.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            prikazZaposlenihTB.Text = string.Empty;

            ICompanyDB proxy = factory.CreateChannel();

            List<Osoba> baza = proxy.Read();

            foreach (var zaposlen in baza)
            {
                if (zaposlen.Prijavljen.Equals(true))
                {
                    prikazZaposlenihTB.Text += zaposlen.ToString() + "\n";
                }
            }
        }

        private void dolazakNaPosao ()
        {
            string[] vreme = trenutni.RadnoVremeStart.Split(':');
            int sati = Int32.Parse(vreme[0]);
            int minuti = Int32.Parse(vreme[1]);

            int rezultat = vremeDolaskaNaPosao.Minute - minuti;

            if ((vremeDolaskaNaPosao.Hour == sati && rezultat > 15) || vremeDolaskaNaPosao.Hour > sati)
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("jovana.medakovic94@gmail.com", "programiranje"),
                    EnableSsl = true
                };
                client.Send(trenutni.Email, trenutni.Email, "Obavestenje", "Zakasnili ste na posao!");
                MessageBox.Show("Proverite mejl!");
            }
        }

        private void promenaLozinke()
        {
            DateTime trenutnovreme = DateTime.Now;
            DateTime lozinka = trenutni.VremeLozinke;

            int razlika = Math.Abs(trenutnovreme.Month - lozinka.Month);

            if (razlika == 6)
            {
                IzmenaPodataka izmena = new IzmenaPodataka(trenutni, factory, 2);
                izmena.ShowDialog();
            }
        }
    }
}
