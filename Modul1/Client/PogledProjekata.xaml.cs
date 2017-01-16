using Biblioteka.Access;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Biblioteka.Data;
using System.ServiceModel;

namespace Client
{
    public partial class PogledProjekata : Window 
    {
		ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
		   new NetTcpBinding(),
		   new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));
        Projekat proj;

        public PogledProjekata()
        {
            InitializeComponent();
			
        }
		

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void povezi(object sender, RoutedEventArgs e)
		{
          
            if (listBoxProj.SelectedItem != null)
			{
                proj = (Projekat)listBoxProj.SelectedItem;



            }
		} 

        private void spoj2(object sender, RoutedEventArgs e) // a ovde pravim instancu outsorsingKompanije i povezujem
        {
            ICompanyDB proxy = factory.CreateChannel();

            if (ListBoxOutsorsing.SelectedItem != null)
            {
                KompanijaOUT kompOut = new KompanijaOUT();


                proj.KompOut = (KompanijaOUT)ListBoxOutsorsing.SelectedItem;
                proxy.ReplaceActionProject(proj);                      /// ovde replaceActionProject 
                MessageBox.Show("Konacno jebeno sam povezaooo ");
            }

        }
    }
}
