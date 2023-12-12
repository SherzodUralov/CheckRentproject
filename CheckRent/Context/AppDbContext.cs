using CheckRent.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckRent.Context
{
    public class AppDbContext : DbContext
    {
       // protected readonly IConfiguration Configuration;

        //public AppDbContext(IConfiguration _configuration) 
        //{
        //    Configuration = _configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("WebCheck"));
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rent>(enity =>
            {
                enity.HasNoKey().ToView("Rent");
            });
        }
        public DbSet<Rent> Rents { get; set; }
    }
}
