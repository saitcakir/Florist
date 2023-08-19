using CicekciEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekciDL.Concrete
{
    public class Context : DbContext
    {
        public Context() : base("FlowerDBConn")
        {
        }

        public DbSet<Flower> Flowers { get; set; }
        public DbSet<FlowerDisplay> FlowerDisplays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
