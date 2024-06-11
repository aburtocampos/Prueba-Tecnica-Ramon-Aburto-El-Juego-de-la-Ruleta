using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WebApplication1.Entities;

namespace WebApplication1.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RuletaEntity> Ruletas { get; set; }


    }
}
