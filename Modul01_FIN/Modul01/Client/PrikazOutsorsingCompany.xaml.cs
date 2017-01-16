﻿using Biblioteka.Access;
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
    /// Interaction logic for PrikazOutsorsingCompany.xaml
    /// </summary>
    public partial class PrikazOutsorsingCompany : Window
    {
        private ChannelFactory<ICompanyDB> factory = new ChannelFactory<ICompanyDB>(
            new NetTcpBinding(), 
            new EndpointAddress("net.tcp://localhost:4000/ICompanyDB"));

        public void PrikaziKompanije()
        {
            ICompanyDB proxy = factory.CreateChannel();
            List<KompanijaOUT> baza = proxy.ReadCompanyOutsorsing();

            foreach (var kompanijaOut in baza)
            {
                ListBoxOutsorsing.Items.Add(kompanijaOut);
            }
        }

        public PrikazOutsorsingCompany()
        {
            InitializeComponent();
            PrikaziKompanije();
        }

        private void ZatvoriProzor(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PoveziFinal(object sender, RoutedEventArgs e)
        { 
        }
  }
}