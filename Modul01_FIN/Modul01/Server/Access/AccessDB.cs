using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class AccessDB : DbContext
    {
        public AccessDB() : base("companyDB") { }

        public DbSet<Osoba> Actions { get; set; }

        public DbSet<KompanijaOUT> ActionsCompanyOut { get; set; }

        public DbSet<Projekat> ActionsProject { get; set; }

    }
}
