using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
    public class Osoba
    {
        private int id;
        private string korIme;
        private string lozinka;
        private string uloga;
        private string radnoVremeStart;
        private string radnoVremeKraj;
        private bool prijavljen;
        private DateTime vremeLozinke;
        private string email;

        public Osoba() { }

        public Osoba(string korIme, string lozinka, string uloga, string pocetakRV, string krajRV, bool prijava, DateTime vremeLozinke, string email)
        {
            this.korIme = korIme;
            this.lozinka = lozinka;
            this.uloga = uloga;
            this.radnoVremeStart = pocetakRV;
            this.radnoVremeKraj = krajRV;
            this.prijavljen = prijava;
            this.vremeLozinke = vremeLozinke;
            this.email = email;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string KorIme
        {
            get { return korIme; }
            set { korIme = value; }
        }

        public string Lozinka
        {
            get { return lozinka; }
            set { lozinka = value; }
        }

        public string Uloga
        {
            get { return uloga; }
            set { uloga = value; }
        }

        public string RadnoVremeStart
        {
            get { return radnoVremeStart; }
            set { radnoVremeStart = value; }
        }

        public string RadnoVremeKraj
        {
            get { return radnoVremeKraj; }
            set { radnoVremeKraj = value; }
        }

        public bool Prijavljen
        {
            get { return prijavljen; }
            set { prijavljen = value; }
        }

        public DateTime VremeLozinke
        {
            get { return vremeLozinke; }
            set { vremeLozinke = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public override string ToString()
        {
            return korIme + " " + uloga;
        }
    }
}
