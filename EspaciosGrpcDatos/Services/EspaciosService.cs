using EspaciosGrpcDatos.Data;
using EspaciosGrpcDatos.Model;
using EspaciosGrpcDatos.Utils;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EspaciosGrpcDatos.Services;

public class EspaciosService(EspaciosContext espaciosContext) : Espacios.EspaciosBase
{
    // Get All
    public override async Task<GetAllEspaciosResponse> GetAllEspacios(GetAllEspaciosRequest request,
        ServerCallContext context)
    {
        var espacios = await espaciosContext.Espacios.Include(e => e.Horarios).ToListAsync();

        var response = new GetAllEspaciosResponse();
        foreach (var espacio in espacios) response.Espacios.Add(EspaciosUtils.ConvertToEspacioMessage(espacio));

        return response;
    }

    // Get By Id
    public override async Task<GetEspacioByIdResponse> GetEspacioById(GetEspacioByIdRequest request,
        ServerCallContext context)
    {
        var espacio = await espaciosContext.Espacios.Include(e => e.Horarios)
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        if (espacio == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Espacio no encontrado"));

        return new GetEspacioByIdResponse { Espacio = EspaciosUtils.ConvertToEspacioMessage(espacio) };
    }

    // Create
    public override async Task<CreateEspacioResponse> CreateEspacio(CreateEspacioRequest request,
        ServerCallContext context)
    {
        var espacio = new Espacio
        {
            Nombre = request.Nombre,
            HoraApertura = TimeSpan.Parse(request.HoraApertura),
            HoraCierre = TimeSpan.Parse(request.HoraCierre),
            Horarios = EspaciosUtils.PopulateHorarios(request.Horarios)
        };

        espaciosContext.Espacios.Add(espacio);
        await espaciosContext.SaveChangesAsync();

        return new CreateEspacioResponse { Espacio = EspaciosUtils.ConvertToEspacioMessage(espacio) };
    }

    // Update
    public override async Task<UpdateEspacioResponse> UpdateEspacio(UpdateEspacioRequest request,
        ServerCallContext context)
    {
        var espacio = await espaciosContext.Espacios.Include(e => e.Horarios)
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        if (espacio == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Espacio no encontrado"));

        espacio.Nombre = request.Nombre;
        espacio.HoraApertura = TimeSpan.Parse(request.HoraApertura);
        espacio.HoraCierre = TimeSpan.Parse(request.HoraCierre);

        // Update horarios
        espaciosContext.Horarios.RemoveRange(espacio.Horarios);
        espacio.Horarios = EspaciosUtils.PopulateHorarios(request.Horarios);

        await espaciosContext.SaveChangesAsync();

        return new UpdateEspacioResponse { Espacio = EspaciosUtils.ConvertToEspacioMessage(espacio) };
    }

    // Delete
    public override async Task<DeleteEspacioResponse> DeleteEspacio(DeleteEspacioRequest request,
        ServerCallContext context)
    {
        var espacio = await espaciosContext.Espacios.Include(e => e.Horarios)
            .FirstOrDefaultAsync(e => e.Id == request.Id);

        if (espacio == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Espacio no encontrado"));

        espaciosContext.Horarios.RemoveRange(espacio.Horarios);

        espaciosContext.Espacios.Remove(espacio);

        await espaciosContext.SaveChangesAsync();

        return new DeleteEspacioResponse { Success = true };
    }
}