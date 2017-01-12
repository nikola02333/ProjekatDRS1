using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
    public class Kompanija
    {
        private int id;
        private string ime;
        private int direktor;
        private List<string> partnerskeKompanije;

        public Kompanija()
        {
            partnerskeKompanije = new List<string>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public int Direktor
        {
            get { return direktor; }
            set { direktor = value; }
        }

        public List<string> PartnerskeKompanije
        {
            get { return partnerskeKompanije; }
            set { partnerskeKompanije = value; }
        }

        public override string ToString()
        {
            return ime;
        }
    }
}
