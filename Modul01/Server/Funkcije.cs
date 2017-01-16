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

            int postoji = -1;

            Osoba o = new Osoba();
            o.KorIme = username;
            o.Lozinka = pass;
            string poruka;
            List<Osoba> baza = CompanyDB.Instance.Read();

            foreach(var zaposlen in baza)
            {
                if(o.KorIme.Equals(zaposlen.KorIme))
                {
                    if(o.Lozinka.Equals(zaposlen.Lozinka))
                    {
                         
                        switch(zaposlen.Uloga)
                        {
                            case "direktor":
                                 poruka = string.Format("Funkcija.SlanjeLoga - Korisnik {0} se uspesno ulogovao", zaposlen.KorIme);
                                Program.Logger.Info(poruka);
                                return 0;
                               
                            case "hr":
                                 poruka = string.Format("Funkcija.SlanjeLoga - Korisnik {0} se uspesno ulogovao", zaposlen.KorIme);
                                Program.Logger.Info(poruka);
                                return 1;
                                
                            case "vlasnikproj":
                                 poruka = string.Format("Funkcija.SlanjeLoga - Korisnik {0} se uspesno ulogovao", zaposlen.KorIme);
                                Program.Logger.Info(poruka);
                                return 2;
                                
                            case "radnik":
                                poruka = string.Format("Funkcija.SlanjeLoga - Korisnik {0} se uspesno ulogovao", zaposlen.KorIme);
                                Program.Logger.Info(poruka);
                                return 3;
                            case "skrmaster":
                                return 4;

                        }
                      
                    }
                }
            }

           
                Program.zaposleni.Add(pass, o);

                return 0;  
            }
        }
    
 }

