using Biblioteka;
using Biblioteka.Access;
using Biblioteka.Data;
using Biblioteka.Interface;
using Server.Access;
using Server.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Server
{
    public class Program
    {
        private static log4net.ILog logger = Logovanje.GetLogger();
        private static Dictionary<string, Osoba> zaposleni = new Dictionary<string, Osoba>();
        private static int iD_Proj = 1;
        private static ServiceHost svc = null;
        private static ServiceHost svc2 = null;

        public static ILog Logger
        {
            get
            {
                return logger;
            }

            set
            {
                logger = value;
            }
        }

        public static int ID_Proj
        {
            get
            {
                return iD_Proj;
            }

            set
            {
                iD_Proj = value;
            }
        }

        public static Dictionary<string, Osoba> Zaposleni
        {
            get
            {
                return zaposleni;
            }

            set
            {
                zaposleni = value;
            }
        }

        public static void Main(string[] args)
        {
            Start();

            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executable);
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AccessDB, Configuration>());

            CompanyDB.Instance.PopunjavanjeTabele();
         
            CompanyDB.Instance.PopunjavanjeBaze();

            Console.WriteLine("\nPocni\n");

            Console.ReadKey(true);
            Stop();
        }

        private static void Start()
        {
            svc = new ServiceHost(typeof(Funkcije));

            svc.AddServiceEndpoint(typeof(IFunkcije),
                new NetTcpBinding(),
                new Uri("net.tcp://localhost:4000/IFunkcije"));

            svc.Open();


            svc2 = new ServiceHost(typeof(CompanyDB));

            svc2.AddServiceEndpoint(typeof(ICompanyDB),
                new NetTcpBinding(),
                new Uri("net.tcp://localhost:4000/ICompanyDB"));

            svc2.Open();

          

            Console.WriteLine("WCF server ready and waiting for requests.");
        }

        private static void Stop()
        {
            svc.Close();
            svc2.Close();
           

            Console.WriteLine("WCF server stopped.");
        }
    }
}
