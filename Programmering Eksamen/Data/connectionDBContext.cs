using Programmering_Eksamen.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; //integrer EntityFramework i filen

namespace Programmering_Eksamen.Data
{
    public class connectionDBContext : DbContext
    {
        private readonly IConfiguration _config;
        public connectionDBContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config.GetConnectionString("Forbindelse"));
        }
    }
}
