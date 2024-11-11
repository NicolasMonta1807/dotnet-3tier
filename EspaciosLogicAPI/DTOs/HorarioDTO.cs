using System.Text.Json.Serialization;

namespace EspaciosLogicAPI.DTOs;

public class HorarioDTO
{
    [JsonPropertyName("hora_inicio")] public string HoraInicio { get; set; }

    [JsonPropertyName("hora_fin")] public string HoraFin { get; set; }

    [JsonPropertyName("capacidad")] public int Capacidad { get; set; }
}