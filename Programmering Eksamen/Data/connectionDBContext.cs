using Programmering_Eksamen.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client; //integrer EntityFramework i filen

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
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; } //ik sikker på om den her bør være her, prøver bare noget

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config.GetConnectionString("Forbindelse"));
        }
    }
}
