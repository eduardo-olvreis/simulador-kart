using KartSimulator.Entities;
using Microsoft.EntityFrameworkCore;

namespace KartSimulator.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
