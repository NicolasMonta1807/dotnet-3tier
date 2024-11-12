using EspaciosBlazorApp.Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EspaciosBlazorApp.Services
{
    public class EspaciosService
    {
        private readonly HttpClient _httpClient;

        public EspaciosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Espacio>> GetEspaciosAsync()
        {
            var espacios = await _httpClient.GetFromJsonAsync<List<Espacio>>("api/Espacios");
            return espacios ?? new List<Espacio>();
        }
    }
}
