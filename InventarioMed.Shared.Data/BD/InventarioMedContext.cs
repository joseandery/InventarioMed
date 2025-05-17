using InventarioMed.Shared.Data.Models;
using InventarioMed.Shared.Models;
using InventarioMed_Console;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMed.Shared.Data.BD
{
    public class InventarioMedContext : IdentityDbContext<AccessUser,AccessRole,int>
    {
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Department> Department { get; set; }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventarioMed_BD_V1;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Departments)
                .WithMany(d  => d.Equipment);
        }
    }
}
