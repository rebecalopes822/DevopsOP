using OdontoPrevAPI.DTOs;

namespace OdontoPrevAPI.Services
{
    public class ViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponseDTO> ConsultarCepAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";
            return await _httpClient.GetFromJsonAsync<ViaCepResponseDTO>(url);
        }
    }
}
