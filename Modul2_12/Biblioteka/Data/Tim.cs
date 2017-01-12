using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
    public class Tim
    {
        private int id;
        private string imeTima;
        private int vodja;
        private List<int> inzenjeri;

        public Tim ()
        {
            inzenjeri = new List<int>();
        }

        public Tim (string ime, int vodja, List<int> inz)
        {
            this.imeTima = ime;
            this.vodja = vodja;
            this.inzenjeri = inz;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string ImeTima
        {
            get { return imeTima; }
            set { imeTima = value; }
        }

        public int Vodja
        {
            get { return vodja; }
            set { vodja = value; }
        }

        public List<int> Inzenjeri
        {
            get { return inzenjeri; }
            set { inzenjeri = value; }
        }

        public override string ToString()
        {
            return imeTima;
        }
    }
}
