using EspaciosGrpcDatos.Model;
using Microsoft.EntityFrameworkCore;

namespace EspaciosGrpcDatos.Data;

public class EspaciosContext : DbContext
{
    public EspaciosContext(DbContextOptions<EspaciosContext> options) : base(options)
    {
    }

    public DbSet<Espacio> Espacios { get; set; }
    public DbSet<Horario> Horarios { get; set; }
}