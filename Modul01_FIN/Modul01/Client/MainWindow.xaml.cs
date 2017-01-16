using Biblioteka.Data;
using Biblioteka.Interface;
using Server;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChannelFactory<IFunkcije> factory = new ChannelFactory<IFunkcije>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IFunkcije"));

     

        public MainWindow()
        {
            InitializeComponent();
            
        }

       

        private void Logovanje(object sender, RoutedEventArgs e)
        {
            IFunkcije proxy = factory.CreateChannel();

            string username = usernameTB.Text;
            string pass = passwordPB.Password;
       

            Osoba zaposlen = new Osoba();
            zaposlen.KorIme = username;
            zaposlen.Lozinka = pass;


            // 0- direktor , 1-hr , 2- radnik , 3-PO , 4 sm
            switch (proxy.SlanjeLoga(username, pass))  
            {
                case 0:
                         Direktor zaposleni = new Direktor(zaposlen);
                    
                    zaposleni.Owner = this;

                    zaposleni.ShowDialog();
                  
                    break;
                case 1:
                    SektorHR hr = new SektorHR(zaposlen);
                    hr.Owner = this;
                    hr.ShowDialog();
                    break;
                case 2:
                    VlasnikProizvoda vl = new VlasnikProizvoda(zaposlen);
                    vl.Owner = this;
                    vl.ShowDialog();
                    break;
                case 3:
                    Radnik radnik = new Radnik(zaposlen);
                    radnik.Owner = this;
                    radnik.ShowDialog();
                    break;
                case 4:
                    
                    break;
                case 5:  
                    Radnik radnik1 = new Radnik(zaposlen);
                    radnik1.Owner = this;
                    radnik1.buttonVodjeTima.Visibility = System.Windows.Visibility.Visible;
                    radnik1.ShowDialog();
                    break;

            }
            
        }
     }
}
