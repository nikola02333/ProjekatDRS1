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
    /// Interaction logic for ProdOwnerCloseProject.xaml
    /// </summary>
    public partial class ProdOwnerCloseProject : Window
    {
        private ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
              new NetTcpBinding(),
              new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));
       
        public ProdOwnerCloseProject()
        {
            InitializeComponent();
            Ipsis();
        }

        private void Ipsis()
        {

            ICompanyDB proxy = factory.CreateChannel();

            List<Projekat> lista_Projekata = new List<Projekat>();
            lista_Projekata = proxy.ReadProjects();
            PogledProjekata pogProj = new PogledProjekata();

            foreach (var p in lista_Projekata)
            {
                listBox.Items.Add(p);
            }


        }

        private void CloseProject_Click(object sender, RoutedEventArgs e)
        {
            Projekat proj;

            ICompanyDB proxy = factory.CreateChannel();

            if (listBox.SelectedItem != null)
            {
                proj = (Projekat)listBox.SelectedItem;
                proj.Zavrsen = true;

                proxy.ReplaceActionProject(proj);    
            }
            Close();
        }
    }
}
