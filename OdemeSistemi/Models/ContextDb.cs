using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdemeSistemi.Models
{
    public class ContextDb: DbContext
    {
        public ContextDb():base("Payment")
        {

        }

        public DbSet<Abone> Abones { get; set; }
        public DbSet<Fatura>Faturas { get; set; }
        public DbSet<Gise> Gises { get; set; }
    }
}