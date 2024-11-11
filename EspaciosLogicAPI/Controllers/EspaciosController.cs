using EspaciosLogicAPI.DTOs;
using EspaciosLogicAPI.GrpcClients;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace EspaciosLogicAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EspaciosController(EspaciosGrpcClient grpcClient) : ControllerBase
{
    // GET: api/espacios
    [HttpGet]
    public async Task<IActionResult> GetAllEspacios()
    {
        var response = await grpcClient.GetAllEspaciosAsync();
        return Ok(response.Espacios);
    }

    // GET: api/espacios/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEspacioById(int id)
    {
        try
        {
            var response = await grpcClient.GetEspacioByIdAsync(id);
            if (response.Espacio == null) return NotFound("El espacio no fue encontrado.");

            return Ok(response.Espacio);
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return NotFound(ex.Status.Detail);
        }
        catch (RpcException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Status.Detail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
        }
    }

    // POST: api/espacios
    [HttpPost]
    public async Task<IActionResult> CreateEspacio([FromBody] CreateEspacioDTO dto)
    {
        var request = new CreateEspacioRequest
        {
            Nombre = dto.Nombre,
            HoraApertura = dto.HoraApertura,
            HoraCierre = dto.HoraCierre,
            Horarios =
            {
                dto.Horarios.Select(h => new HorarioMessage
                {
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin,
                    Capacidad = h.Capacidad
                })
            }
        };

        var response = await grpcClient.CreateEspacioAsync(request);
        return CreatedAtAction(nameof(GetEspacioById), new { id = response.Espacio.Id }, response.Espacio);
    }

    // PUT: api/espacios/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEspacio(int id, [FromBody] UpdateEspacioDTO dto)
    {
        if (id != dto.Id) return BadRequest("El id de la solicitud no corresponse con el id del espacio");

        var request = new UpdateEspacioRequest
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
            HoraApertura = dto.HoraApertura,
            HoraCierre = dto.HoraCierre,
            Horarios =
            {
                dto.Horarios.Select(h => new HorarioMessage
                {
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin,
                    Capacidad = h.Capacidad
                })
            }
        };

        var response = await grpcClient.UpdateEspacioAsync(request);
        return Ok(response.Espacio);
    }

    // DElETE: api/espacios/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEspacio(int id)
    {
        try
        {
            var response = await grpcClient.DeleteEspacioAsync(id);
            if (!response.Success)
                return BadRequest("No se pudo eliminar el espacio");
            return NoContent();
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return NotFound(ex.Status.Detail);
        }
        catch (RpcException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Status.Detail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error inesperado: " + ex.Message);
        }
    }
}