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
using Server;

namespace Client
{
    /// <summary>
    /// Interaction logic for VlasnikProizvoda.xaml
    /// </summary>
    public partial class VlasnikProizvoda : Window
    {
        ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
               new NetTcpBinding(),
               new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

        Osoba z;
        Osoba trenutni;
        DateTime vremeDolaskaNaPosao;
        public VlasnikProizvoda(Osoba zaposlen)
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
            //         dolazakNaPosao();                               

            promenaLozinke();

            
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




        private void KreirajProjekat(object sender, RoutedEventArgs e)
        {

            
                Projekat proj = new Projekat(textBoxIme.Text, textBoxOpis.Text,
                textBoxKriterijunm.Text, dataPickerPoc.SelectedDate.Value, dataPickerKraj.SelectedDate.Value
                , textBoxKorisnicaPrica.Text, int.Parse(textBoxTezina.Text), textBoxZadaci.Text, Program.ID_Proj);
            
           

            Program.ID_Proj++;
            bool x= ubaciUbazuProjekat(proj);

        }
        public bool ubaciUbazuProjekat(Projekat proj)
        {
            ICompanyDB proxy = factory.CreateChannel();

            if( proxy.AddActionProject(proj))
            {
                MessageBox.Show("Projekat uspesno sacuvan u bazi :D ");
            }

            return true;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ICompanyDB proxy = factory.CreateChannel();

            trenutni.Prijavljen = false;
            proxy.ReplaceAction(z, trenutni);
            this.Close();
        }
    }
}
