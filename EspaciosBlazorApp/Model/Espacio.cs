namespace EspaciosBlazorApp.Model;

public class Espacio
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public TimeSpan HoraApertura { get; set; }
    public TimeSpan HoraCierre { get; set; }
    public List<Horario> Horarios { get; set; } = new List<Horario>();
}