using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Estacionat.Models;

namespace Estacionat.Models
{
    public class EstacionatContext : DbContext
    {
        public EstacionatContext (DbContextOptions<EstacionatContext> options)
            : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //   modelBuilder.Entity<Department>().ToTable("")
        //}

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }


    }
}
