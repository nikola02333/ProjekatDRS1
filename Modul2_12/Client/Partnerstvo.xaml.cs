using Biblioteka.Access;
using Biblioteka.Data;
using Biblioteka.Interface;
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
    /// Interaction logic for Partnerstvo.xaml
    /// </summary>
    public partial class Partnerstvo : Window
    {
        string hiring;
        Kompanija outsourcing;
        Projekat projekat;

        ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

        ChannelFactory<IKomunikacija> factory2 = new ChannelFactory<IKomunikacija>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IKomunikacija"));

        public Partnerstvo(string hiring, Kompanija outsourcing)
        {
            InitializeComponent();

            this.hiring = hiring;
            this.outsourcing = outsourcing;

            label1.Visibility = Visibility.Hidden;
            timoviTB.IsEnabled = false;

            imeKompanijeTB.Text = hiring;
            label.Content = "Zahtev za partnerstvo";
            label.FontSize = 20;
        }

        public Partnerstvo(Projekat projekat, Kompanija outsourcing)
        {
            InitializeComponent();

            this.projekat = projekat;
            this.outsourcing = outsourcing;

            label1.Visibility = Visibility.Visible;
            timoviTB.IsEnabled = true;

            label.Content = "Zahtev za projekat";
            label.FontSize = 20;

            imeKompanijeTB.Text = projekat.Ime;
            imeKompanijeTB.Text += projekat.Opis;
            imeKompanijeTB.Text += projekat.Pocetak.ToString();
            imeKompanijeTB.Text += projekat.Kraj.ToString();

          //  pregledTimova();
        }

     

        private void potvrdaB_Click (object sender, RoutedEventArgs e)
        {
            ICompanyDB proxy = factory.CreateChannel();
            IKomunikacija proxy2 = factory2.CreateChannel();

            if (timoviTB.IsEnabled == true)
            {
                if (prihvatiCB.IsChecked == true && odbijCB.IsChecked == false)
                {
                    //cuvati u nekoj listi il dictionary

                    proxy2.OdgovorNaProjekat(true, projekat, outsourcing.Ime);
                }
                else
                {
                    proxy2.OdgovorNaProjekat(false, projekat, outsourcing.Ime);
                }
            }
            else
            {
                if (prihvatiCB.IsChecked == true && odbijCB.IsChecked == false)
                {
                    outsourcing.PartnerskeKompanije.Add(hiring);
                    proxy.ReplaceActionCompany(outsourcing);

                    proxy2.OdgovorNaPartnerstvo(true, hiring, outsourcing.Ime);
                }
                else
                {
                    proxy2.OdgovorNaPartnerstvo(false, hiring, outsourcing.Ime);
                }
            }

            this.Close();
        }

          private void timoviTB_SelectionChanged (object sender, SelectionChangedEventArgs e)
          {
              //projekat.Tim = (Tim)timoviTB.SelectedItem;
        }
    }
}
