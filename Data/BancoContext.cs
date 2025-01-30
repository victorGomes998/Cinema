
using Cinema.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> opts) : base(opts) { }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Estabelecimento> Estabelecimentos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
}