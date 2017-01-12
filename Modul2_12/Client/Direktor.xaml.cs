using Biblioteka.Access;
using Biblioteka.Data;
using Biblioteka.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Direktor.xaml
    /// </summary>
    public partial class Direktor : Window
    {
        Osoba z;
        Osoba prijavljeni;
        DateTime vremeDolaskaNaPosao;
        Thread zahtevPartnerstvo;
        List<string> kompanije;
        string hiring;
        Kompanija outsourcing;
        Dictionary<string, Projekat> projekat;

        ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

		

        ChannelFactory<IFunkcije> factory1 = new ChannelFactory<IFunkcije>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IFunkcije"));

        public Direktor(Osoba zaposlen)
        {
            InitializeComponent();

            ICompanyDB proxy = factory.CreateChannel();
            List<Osoba> privremena = proxy.Read();

            foreach(Osoba o in privremena)
            {
                if(o.KorIme.Equals(zaposlen.KorIme))
                {
                    if(o.Lozinka.Equals(zaposlen.Lozinka))
                    {
                        this.z = o;
                        break;
                    }
                }
            }

            this.prijavljeni = z;
            prijavljeni.Prijavljen = true;
            proxy.ReplaceAction(z, prijavljeni); // ovde menja  da je zaposlen dosao na posao tj. postavlja vrednost na  TRUE u bazi podataka !!!

            this.vremeDolaskaNaPosao = DateTime.Now;
           //dolazakNaPosao();                                    // OTKOMENTARISATI!!!!!!!!!!!!!!!!

            promenaLozinke();

            pregledZaposlenih();
           // pregledTimova();
           // pregledPartnerskihKopmanija();

           // zahtevPartnerstvo = new Thread(new ThreadStart(zahtev));
           // zahtevPartnerstvo.Start();
        }

       /* private void zahtev()
        {
            while(true)
            {
                ICompanyDB proxy = factory.CreateChannel();
                IFunkcije proxy2 = factory1.CreateChannel();

                kompanije = proxy2.ProveraZahteva();

                if(kompanije.Count != 0)
                {
                    hiring = kompanije[0];

                    List<Kompanija> baza = proxy.ReadCompany();

                    foreach (Kompanija kompanija in baza)
                    {
                        if (kompanija.Ime.Equals(kompanije[1]))
                        {
                            if(kompanija.Direktor.Equals(prijavljeni.Id))
                            {
                                outsourcing = kompanija;

                                Dispatcher.Invoke(() =>
                                {
                                    label2.Visibility = Visibility.Visible;
                                    label2.Content = "Imate zahtev za partnerstvo!";
                                    label2.Foreground = Brushes.Red;

                                    partnerstvo.IsEnabled = true;
                                });
                            }
                        }
                    }
                }

                projekat = proxy2.ProveraZahtevaProjekat();

                if(projekat.Count !=0)
                {
                    List<Kompanija> baza = proxy.ReadCompany();

                    foreach (Kompanija kompanija in baza)
                    {
                        if (kompanija.Ime.Equals(projekat.Keys))
                        {
                            if (kompanija.Direktor.Equals(prijavljeni.Id))
                            {
                                outsourcing = kompanija;

                                Dispatcher.Invoke(() =>
                                {
                                    label2.Visibility = Visibility.Visible;
                                    label2.Content = "Imate zahtev za projekat!";
                                    label2.Foreground = Brushes.Red;

                                    partnerstvo.IsEnabled = true;
                                });
                            }
                        }
                    }
                }

                Thread.Sleep(5000);
            }
        } */

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ICompanyDB proxy = factory.CreateChannel();

            prijavljeni.Prijavljen = false;
            proxy.ReplaceAction(z, prijavljeni);

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
                if (zaposlen.KorIme.Equals(prijavljeni.KorIme))
                {
                    if (zaposlen.Lozinka.Equals(prijavljeni.Lozinka))
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

      /*  private void pregledTimova()
        {
            timoviTB.SelectedItem = null;
            timoviTB.Items.Clear();

            ICompanyDB proxy = factory.CreateChannel();

            List<Tim> baza = proxy.ReadTim();

            foreach (var tim in baza)
            {
                timoviTB.Items.Add(tim);       
            }
        }*/

       /* private void pregledPartnerskihKopmanija ()
        {
            kompanijeCB.SelectedItem = null;
            kompanijeCB.Items.Clear();

            ICompanyDB proxy = factory.CreateChannel();

            List<Kompanija> baza = proxy.ReadCompany();

            foreach (Kompanija kompanija in baza)
            {
                if (kompanija.Direktor.Equals(prijavljeni.Id))
                {
                    foreach(string hiring in kompanija.PartnerskeKompanije)
                    {
                        kompanijeCB.Items.Add(hiring);
                    }
                }
            }
        }
        */
        private void DodavanjeNovogZaposlenog(object sender, RoutedEventArgs e)
        {
            NoviZaposleni novi = new NoviZaposleni(factory);
            novi.Owner = this;
            novi.ShowDialog();
        }

        private void sviZaposleniCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sviZaposleniCB.SelectedItem == null))
            {
                IzmenaPodataka izmena = new IzmenaPodataka((Osoba)sviZaposleniCB.SelectedItem, factory, 0);
                izmena.Owner = this;
                izmena.ShowDialog();
            }
        }

        private void IzmenaSpostvenihPodataka(object sender, RoutedEventArgs e)
        {
            IzmenaPodataka izmena = new IzmenaPodataka(prijavljeni, factory, 1);
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

        private void dolazakNaPosao()
        {
            string[] vreme = prijavljeni.RadnoVremeStart.Split(':');
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
                client.Send(prijavljeni.Email, prijavljeni.Email, "Obavestenje", "Zakasnili ste na posao!");
                MessageBox.Show("Proverite mejl!");
            }
        }

        private void promenaLozinke()
        {
            DateTime trenutnovreme = DateTime.Now;
            DateTime lozinka = prijavljeni.VremeLozinke;

            int razlika = Math.Abs(trenutnovreme.Month - lozinka.Month);

            if(razlika == 6)
            {
                IzmenaPodataka izmena = new IzmenaPodataka(prijavljeni, factory, 2);
                izmena.ShowDialog();
            }
        }
        private void partnerstvo_Click(object sender, RoutedEventArgs e) { }
		
		/// /////////////////////////////////////////////////////////////////
		

		private void Ucitaj_OutsorsingKompanije(object sender, RoutedEventArgs e)
		{
			PrikazOutsorsingCompany window = new PrikazOutsorsingCompany();
			window.Show();

			
		}

        private void Ucitavanje_projekata_Iz_Baze(object sender, RoutedEventArgs e)
        {

            ICompanyDB proxy = factory.CreateChannel();

            List<Projekat> lista_Projekata = new List<Projekat>();
            lista_Projekata =  proxy.ReadProjects();
            PogledProjekata pogProj = new PogledProjekata();
            foreach(var p in lista_Projekata)
            {
                pogProj.listBox.Items.Add(p.ToString() );
            }
            //pogProj.listBox.DataContext = lista_Projekata;
           

            pogProj.Show();



        }
        /* private void partnerstvo_Click (object sender, RoutedEventArgs e)
{
    if (kompanije.Count != 0)
    {
        Partnerstvo part = new Partnerstvo(hiring, outsourcing);
        part.Owner = this;
        part.ShowDialog();
    }
    else
    {
        Partnerstvo part = new Partnerstvo(projekat[outsourcing.Ime], outsourcing);
        part.Owner = this;
        part.ShowDialog();
    }

    partnerstvo.IsEnabled = false;
    label2.Visibility = Visibility.Hidden;
    // pregledPartnerskihKopmanija();
}*/
    }
}
