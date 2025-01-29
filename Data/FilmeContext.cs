
using Cinema.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts) { }

    public DbSet<Filme> Filme { get; set; }
}