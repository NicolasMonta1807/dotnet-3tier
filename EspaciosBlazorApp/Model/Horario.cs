namespace EspaciosBlazorApp.Model;

public class Horario
{
    public int Id { get; set; }
    public int EspacioId { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public int Capacidad { get; set; }
}