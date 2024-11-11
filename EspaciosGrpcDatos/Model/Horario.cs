using System.ComponentModel.DataAnnotations.Schema;

namespace EspaciosGrpcDatos.Model;

[Table("horarios")]
public class Horario
{
    [Column("id")] public int Id { get; set; }

    [Column("espacio_id")] public int EspacioId { get; set; }

    [Column("hora_inicio")] public TimeSpan HoraInicio { get; set; }
    [Column("hora_fin")] public TimeSpan HoraFin { get; set; }
    [Column("capacidad")] public int Capacidad { get; set; }

    [ForeignKey("EspacioId")] public Espacio Espacio { get; set; }
}