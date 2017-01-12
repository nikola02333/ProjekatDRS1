using Biblioteka.Data;
using Biblioteka.Interface;
using Server.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Funkcije : IFunkcije
    {
        private static List<string> kompanije = new List<string>();
        List<string> pomocnaLista = new List<string>();

        private static Dictionary<string, Projekat> projekat = new Dictionary<string, Projekat>();
        Dictionary<string, Projekat> pomocna = new Dictionary<string, Projekat>();

        public int SlanjeLoga(string username, string pass)
        {
            Console.WriteLine(username + " " + pass);

            bool postoji = false;

            Osoba o = new Osoba();
            o.KorIme = username;
            o.Lozinka = pass;

            List<Osoba> baza = CompanyDB.Instance.Read();

            foreach(var zaposlen in baza)
            {
                if(o.KorIme.Equals(zaposlen.KorIme))
                {
                    if(o.Lozinka.Equals(zaposlen.Lozinka))
                    {
                        postoji = true; 
                        switch(zaposlen.Uloga)
                        {
                            case "direktor":
                                return 0;
                            case "hr":
                                return 1;
                            case "vlasnikproj":
                                return 2;
                            case "radnik":
                                return 3;
                            case "skrmaster":
                                return 4;

                        }

                        /*
						 * ako postoji onda vrati povratnu vrednost tipa: 0 za ddirektora
						 *												  1 za Hr			
						 *												  2 za radnika
						 *												  3 za production Ownera
						 *												  4 za skram Mastera
						 */
                        break;
                    }
                }
            }

            if (postoji)
            {
                return 0;  //
            }
            else
            {
                Program.zaposleni.Add(pass, o);

                return 0;  // 
            }
        }

        public void ZahtevZaPartnerstvo(string hiring, string outsourcing)
        {
            kompanije.Add(hiring);
            kompanije.Add(outsourcing);
        }

        public List<string> ProveraZahteva()
        {
            pomocnaLista = kompanije;
            kompanije = new List<string>();

            return pomocnaLista; 
        }

        public void ZahtevZaProjekat (Projekat proj, string outsourcing)
        {
            projekat.Add(outsourcing, proj);
        }

        public Dictionary<string, Projekat> ProveraZahtevaProjekat()
        {
            pomocna = projekat;
            projekat = new Dictionary<string, Projekat>();

            return pomocna;
        }

    }
}
