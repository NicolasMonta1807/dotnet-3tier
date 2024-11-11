using System.Text.Json.Serialization;

namespace EspaciosLogicAPI.DTOs;

public class UpdateEspacioDTO
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("nombre")] public string Nombre { get; set; }

    [JsonPropertyName("hora_apertura")] public string HoraApertura { get; set; }

    [JsonPropertyName("hora_cierre")] public string HoraCierre { get; set; }

    [JsonPropertyName("horarios")] public List<HorarioDTO> Horarios { get; set; }
}