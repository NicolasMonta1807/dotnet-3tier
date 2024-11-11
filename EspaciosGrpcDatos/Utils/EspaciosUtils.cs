using EspaciosGrpcDatos.Model;

namespace EspaciosGrpcDatos.Utils;

public static class EspaciosUtils
{
    public static EspacioMessage ConvertToEspacioMessage(Espacio espacio)
    {
        var espacioGrpc = new EspacioMessage
        {
            Id = espacio.Id,
            Nombre = espacio.Nombre,
            HoraApertura = espacio.HoraApertura.ToString(),
            HoraCierre = espacio.HoraCierre.ToString()
        };

        foreach (var horario in espacio.Horarios)
            espacioGrpc.Horarios.Add(new HorarioMessage
            {
                Id = horario.Id,
                HoraInicio = horario.HoraInicio.ToString(),
                HoraFin = horario.HoraFin.ToString(),
                Capacidad = horario.Capacidad
            });

        return espacioGrpc;
    }

    public static List<Horario> PopulateHorarios(IEnumerable<HorarioMessage> horarioMessages)
    {
        return horarioMessages.Select(h => new Horario
        {
            HoraInicio = TimeSpan.Parse(h.HoraInicio),
            HoraFin = TimeSpan.Parse(h.HoraFin),
            Capacidad = h.Capacidad
        }).ToList();
    }
}