using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data
{
	public class KompanijaOUT
	{
		public KompanijaOUT() { }
		public KompanijaOUT(string i, int id,string por, string ip)
		{
			ime = i;
			Id = id;
			port = por;
			ipaddr = ip;
		}

		string ime;
		int id;
		string port;
		string ipaddr;

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

		public string Ipaddr
		{
			get { return ipaddr; }
			set { ipaddr = value; }
		}
		public override string ToString()
		{
			return Ime + Ipaddr + Id;

		}
	}
}
