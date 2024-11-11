using EspaciosGrpcDatos.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EspaciosGrpcDatos.Services;

public class EspaciosService : Espacios.EspaciosBase
{
    private readonly EspaciosContext _context;

    public EspaciosService(EspaciosContext context)
    {
        _context = context;
    }

    public override async Task<GetAllEspaciosResponse> GetAllEspacios(GetAllEspaciosRequest request,
        ServerCallContext context)
    {
        var espacios = await _context.Espacios.Include(e => e.Horarios).ToListAsync();

        var response = new GetAllEspaciosResponse();
        foreach (var espacio in espacios)
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

            response.Espacios.Add(espacioGrpc);
        }

        return response;
    }
}