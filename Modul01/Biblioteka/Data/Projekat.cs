using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
    public class Projekat
    {
        private string ime;
        private string opis;
        private string kriterijum;
        private DateTime pocetak;
        private DateTime kraj;
        private string korPrice;
        private int tezina;
        private string zadaci;
        private int id;
        private bool zavrsen;


        private KompanijaOUT kompOut;

        public Projekat() { }

        public Projekat(string ime, string opis, string kriterijum, DateTime pocetak, DateTime kraj, string korPrice, int tezina, string zadaci, int id, bool za)
        {
            this.Ime = ime;
            this.Opis = opis;
            this.Kriterijum = kriterijum;
            this.Pocetak = pocetak;
            this.Kraj = kraj;
            this.KorPrice = korPrice;
            this.Tezina = tezina;
            this.Zadaci = zadaci;
            this.id = id;
            Zavrsen = za;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                opis = value;
            }
        }

        public string Kriterijum
        {
            get
            {
                return kriterijum;
            }

            set
            {
                kriterijum = value;
            }
        }

        public DateTime Pocetak
        {
            get
            {
                return pocetak;
            }

            set
            {
                pocetak = value;
            }
        }

        public DateTime Kraj
        {
            get
            {
                return kraj;
            }

            set
            {
                kraj = value;
            }
        }

        public string KorPrice
        {
            get
            {
                return korPrice;
            }

            set
            {
                korPrice = value;
            }
        }

        public int Tezina
        {
            get
            {
                return tezina;
            }

            set
            {
                tezina = value;
            }
        }

        public string Zadaci
        {
            get
            {
                return zadaci;
            }

            set
            {
                zadaci = value;
            }
        }

        public KompanijaOUT KompOut
        {
            get
            {
                return kompOut;
            }

            set
            {
                kompOut = value;
            }
        }

        public bool Zavrsen
        {
            get
            {
                return zavrsen;
            }

            set
            {
                zavrsen = value;
            }
        }

        public override string ToString()
        {
            return Id + " " + Ime + " " + Opis + " " + Kriterijum;
        }
    }
}
