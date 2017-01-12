using Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IFunkcija> factory = new ChannelFactory<IFunkcija>(
            new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IFunkcija"));


            IFunkcija proxy = factory.CreateChannel();

            Console.WriteLine("Unesite nesto sto ce biti poslato ka serverru");
            string poruka = Console.ReadLine();
            proxy.Ispis(poruka);
            Console.ReadLine();
        }
    }
}
