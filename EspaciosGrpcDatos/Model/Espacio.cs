using System.ComponentModel.DataAnnotations.Schema;

namespace EspaciosGrpcDatos.Model;

[Table("espacios")]
public class Espacio
{
    [Column("id")] public int Id { get; set; }

    [Column("nombre")] public string Nombre { get; set; }

    [Column("hora_apertura")] public TimeSpan HoraApertura { get; set; }

    [Column("hora_cierre")] public TimeSpan HoraCierre { get; set; }

    public List<Horario> Horarios { get; set; }
}