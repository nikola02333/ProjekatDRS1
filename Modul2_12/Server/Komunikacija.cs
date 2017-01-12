using Biblioteka.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Data;
using Server.Access;

namespace Server
{
    public class Komunikacija : IKomunikacija
    {
        Funkcije f = new Funkcije();

        public void OdgovorNaPartnerstvo(bool potvrda, string hiring, string outsourcing)
        {
            
        }

        public void OdgovorNaProjekat (bool potvrda, Projekat projekat, string outsourcing)
        {

        }

        public void PartnerskiZahtev(string hiring, string outsourcing)
        {
            f.ZahtevZaPartnerstvo(hiring, outsourcing);
        }

        public void ZahtevProjekat(Projekat projekat, string outsourcing)
        {
            f.ZahtevZaProjekat(projekat, outsourcing);
        }
    }
}
