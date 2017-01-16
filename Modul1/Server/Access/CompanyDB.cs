using Biblioteka.Access;
using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Access
{
    public class CompanyDB : ICompanyDB
    {
        private static ICompanyDB myDB;

        Osoba o1 = new Osoba("Dunja", "dunja", "radnik", "9:00", "14:00", false, DateTime.Now, "elguaje358@gmail.com");
        Osoba o2 = new Osoba("Nikola", "nikola", "radnik", "8:00", "14:00", false, DateTime.Parse("07/10/2010"), "svetabombas@live.com");
        Osoba o3 = new Osoba("Tijana", "tiki", "radnik", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o4 = new Osoba("Milana", "milana", "radnik", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o5 = new Osoba("Aca", "aca", "radnik", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o6 = new Osoba("Nina", "nina", "vlasnikproj", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o7 = new Osoba("Nesa", "nesa", "radnik", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o8 = new Osoba("Miki", "miki", "radnik", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o9 = new Osoba("Jovana", "jole", "direktor", "9:00", "14:00", false, DateTime.Now, "neko1118@hotmail.com");
        Osoba o10 = new Osoba("Suzana", "suza", "direktor", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o11 = new Osoba("Jova", "jova", "direktor", "9:00", "14:00", false, DateTime.Now, "jovanamedakovic@hotmail.com");
        Osoba o12 = new Osoba("Sava", "sava", "hr", "9:00", "14:00", false, DateTime.Now, "jovana.medakovic94@gmail.com");

        public static ICompanyDB Instance
        {
            get
            {
                if (myDB == null)
                    myDB = new CompanyDB();

                return myDB;
            }
            set
            {
                if (myDB == null)
                    myDB = value;
            }
        }

        public bool AddAction(Osoba action)
        {
            using (var access = new AccessDB())
            {
                access.Actions.Add(action);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

       

        


        public bool ReplaceAction(Osoba zaposlen, Osoba izmenjen)
        {
            using (var access = new AccessDB())
            {
                foreach (var osoba in access.Actions)
                {
                    if (osoba.KorIme.Equals(zaposlen.KorIme))
                    {
                        if (osoba.Lozinka.Equals(zaposlen.Lozinka))
                        {
                            osoba.KorIme = izmenjen.KorIme;
                            osoba.Lozinka = izmenjen.Lozinka;
                            osoba.Uloga = izmenjen.Uloga;
                            osoba.RadnoVremeKraj = izmenjen.RadnoVremeKraj;
                            osoba.RadnoVremeStart = izmenjen.RadnoVremeStart;
                            osoba.Prijavljen = izmenjen.Prijavljen;
                            osoba.VremeLozinke = izmenjen.VremeLozinke;
                            osoba.Email = izmenjen.Email;
                            break;
                        }
                    }
                }

                int i = access.SaveChanges();
                if (i > 0)
                    return true;
                return false;
            }
        }

      

        public bool PopunjavanjeTabele()
        {
            using (var access = new AccessDB())
            {
                if (access.Actions.Count() == 0)
                {
                    AddAction(o1);
                    AddAction(o2);
                    AddAction(o3);
                    AddAction(o4);
                    AddAction(o5);
                    AddAction(o6);
                    AddAction(o7);
                    AddAction(o8);
                    AddAction(o9);
                    AddAction(o10);
                    AddAction(o11);
                    AddAction(o12);
                }
            }

            return true;
        }


        public List<Osoba> Read()
        {
            List<Osoba> podaciIzBaze = new List<Osoba>();

            using (var access = new AccessDB())
            {
                foreach (var osoba in access.Actions)
                {
                    Console.WriteLine(osoba);
                    podaciIzBaze.Add(osoba);
                }

                return podaciIzBaze;
            }
        }


      
        //////////////////////////////////////////////////////////////
        public bool ActionsCompanyOut(KompanijaOUT action)
        {
            using (var access = new AccessDB())
            {
                access.ActionsCompanyOut.Add(action);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }

        public bool PopunjavanjeBaze()
        {
            using (var access = new AccessDB())
            {
                if (access.ActionsCompanyOut.Count() == 0)
                {
                    ActionsCompanyOut(new KompanijaOUT
                    {
                        Ime = "OutSorsing_1",
                        Id = 1,
                        Ipaddr = "1811994",
                    });
                    ActionsCompanyOut(new KompanijaOUT
                    {
                        Ime = "OutSorsing_2",
                        Id = 2,
                        Ipaddr = "1811995",
                    });
                    return true;
                }
                return false;
            }
        }
        ///////// citanje iz baze za Outsorsing
        public List<KompanijaOUT> ReadCompanyOutsorsing()
        {
            List<KompanijaOUT> podaciIzBazeOutsorsing = new List<KompanijaOUT>();

            using (var access = new AccessDB())
            {
                foreach (var kompanijaout in access.ActionsCompanyOut)
                {
                    //Console.WriteLine(kompanijaout);
                    podaciIzBazeOutsorsing.Add(kompanijaout);
                }

                return podaciIzBazeOutsorsing;
            }
        }
        /////////////////////////////////////
        /* sad isto kao i za Outsorsing Kompaniju */
        public bool AddActionProject(Projekat action)
        {
            using (var access = new AccessDB())
            {
                access.ActionsProject.Add(action);
                int i = access.SaveChanges();

                if (i > 0)
                    return true;
                return false;
            }
        }
        public List<Projekat> ReadProjects()
        {
            List<Projekat> podaciIzBazeProjekti = new List<Projekat>();

            using (var access = new AccessDB())
            {
                foreach (var projekat in access.ActionsProject)
                {
                    //Console.WriteLine(kompanijaout);
                    podaciIzBazeProjekti.Add(projekat);
                }

                return podaciIzBazeProjekti;
            }


        }

        public bool ReplaceActionProject(Projekat projekat)
        {

            using (var access = new AccessDB())
            {
                foreach (var proj in access.ActionsProject)
                {
                    if (proj.Id.Equals(projekat.Id))
                    {
                        proj.Ime = projekat.Ime;
                        proj.KompOut = projekat.KompOut;

                        break;
                    }
                }

                int i = access.SaveChanges();
                if (i > 0)
                    return true;
                return false;
            }
        }
    }
}
