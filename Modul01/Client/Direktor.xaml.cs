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
        private Osoba z;
        private Osoba prijavljeni;
        private DateTime vremeDolaskaNaPosao;
        
     
        private ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

        private ChannelFactory<IFunkcije> factory1 = new ChannelFactory<IFunkcije>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IFunkcije"));

        public Direktor(Osoba zaposlen)
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

            this.prijavljeni = z;
            prijavljeni.Prijavljen = true;
            proxy.ReplaceAction(z, prijavljeni); 

            this.vremeDolaskaNaPosao = DateTime.Now;
            DolazakNaPosao();                                

            PromenaLozinke();

            PregledZaposlenih();

        
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ICompanyDB proxy = factory.CreateChannel();

            prijavljeni.Prijavljen = false;
            proxy.ReplaceAction(z, prijavljeni);

            this.Close();
        }

        private void PregledZaposlenih()
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

      
        private void DodavanjeNovogZaposlenog(object sender, RoutedEventArgs e)
        {
            NoviZaposleni novi = new NoviZaposleni(factory);
            novi.Owner = this;
            novi.ShowDialog();
        }

        private void SviZaposleniCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void DolazakNaPosao()
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

        private void PromenaLozinke()
        {
            DateTime trenutnovreme = DateTime.Now;
            DateTime lozinka = prijavljeni.VremeLozinke;

            int razlika = Math.Abs(trenutnovreme.Month - lozinka.Month);

            if (razlika == 6)
            {
                IzmenaPodataka izmena = new IzmenaPodataka(prijavljeni, factory, 2);
                izmena.ShowDialog();
            }
        }
        private void Partnerstvo_Click(object sender, RoutedEventArgs e) { }
        
        
    

        private void Ucitaj_OutsorsingKompanije(object sender, RoutedEventArgs e)
        {
            PrikazOutsorsingCompany window = new PrikazOutsorsingCompany();
            window.Show();

            
        }

        private void Ucitavanje_projekata_Iz_Baze(object sender, RoutedEventArgs e)
        {

            ICompanyDB proxy = factory.CreateChannel();

            List<Projekat> lista_Projekata = new List<Projekat>();
            lista_Projekata = proxy.ReadProjects();
            PogledProjekata pogProj = new PogledProjekata();

            foreach (var p in lista_Projekata)
            {
                pogProj.listBoxProj.Items.Add(p);
            }
          
            List<KompanijaOUT> lista_OutsorsingKompanija = proxy.ReadCompanyOutsorsing();
            foreach (var outS in lista_OutsorsingKompanija)
                {
                pogProj.ListBoxOutsorsing.Items.Add(outS);
                }

            pogProj.Show();

        }
        
    }
}