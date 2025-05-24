

using Microsoft.EntityFrameworkCore;
using mottu_spot.Model;

namespace mottu_spot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.Placa).IsUnique();

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)
                .WithMany(p => p.Motos)
                .HasForeignKey(m => m.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Dispositivo)
                .WithOne(d => d.Moto)
                .HasForeignKey<Dispositivo>(d => d.MotoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dispositivo>()
                .HasOne(d => d.Moto)
                .WithOne(m => m.Dispositivo)
                .HasForeignKey<Dispositivo>(d => d.MotoId);

            modelBuilder.Entity<Patio>()
                .HasOne(p => p.Endereco)
                .WithOne()
                .HasForeignKey<Patio>(p => p.EnderecoId);
        }
    }
}
