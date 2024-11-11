using System.Text.Json.Serialization;

namespace EspaciosLogicAPI.DTOs;

public class CreateEspacioDTO
{
    [JsonPropertyName("nombre")] public string Nombre { get; set; }

    [JsonPropertyName("hora_apertura")] public string HoraApertura { get; set; }

    [JsonPropertyName("hora_cierre")] public string HoraCierre { get; set; }

    [JsonPropertyName("horarios")] public List<HorarioDTO> Horarios { get; set; }
}