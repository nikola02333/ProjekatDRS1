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
    /// Interaction logic for NoviZaposleni.xaml
    /// </summary>
    public partial class NoviZaposleni : Window
    {
        ChannelFactory<ICompanyDB> factory;

        public NoviZaposleni(ChannelFactory<ICompanyDB> fact)
        {
            InitializeComponent();

            this.factory = fact;

            PopunjavanjeCB();
        }

        private void PopunjavanjeCB()
        {
            ulogaCB.Items.Add("radnik");
            ulogaCB.Items.Add("direktor");
            ulogaCB.Items.Add("hr");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string korIme = korImeTB.Text.Trim();
            string lozinka = lozinkaTB.Text.Trim();
            string uloga = (string)ulogaCB.SelectedItem;
            string pocetakRV = prvTB.Text.Trim();
            string krajRV = krvTB.Text.Trim();
            bool prijava = false;
            DateTime vremeLozinke = DateTime.Now;
            string email = emailTB.Text.Trim();

            Osoba o = new Osoba(korIme, lozinka, uloga, pocetakRV, krajRV, prijava, vremeLozinke, email);

            ICompanyDB proxy = factory.CreateChannel();
            proxy.AddAction(o);

            this.Close();
        }
    }
}
