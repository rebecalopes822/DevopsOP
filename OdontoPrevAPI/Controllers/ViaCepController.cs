using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.DTOs;
using OdontoPrevAPI.Services;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador responsável por consultar endereços via serviço externo (ViaCEP).
    /// </summary>
    [ApiController]
    [Route("api/cep")]
    public class ViaCepController : ControllerBase
    {
        private readonly ViaCepService _viaCepService;

        public ViaCepController(ViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        /// <summary>
        /// Consulta um endereço brasileiro a partir do CEP informado.
        /// </summary>
        /// <param name="cep">CEP no formato de 8 dígitos (ex: 01001000)</param>
        /// <returns>Objeto com os dados do endereço ou erro de validação</returns>
        /// <response code="200">Endereço encontrado com sucesso</response>
        /// <response code="400">CEP inválido (diferente de 8 dígitos)</response>
        /// <response code="404">CEP não encontrado</response>
        [HttpGet("{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
                return BadRequest("CEP inválido. Informe 8 dígitos numéricos.");

            var resultado = await _viaCepService.ConsultarCepAsync(cep);

            if (resultado == null || resultado.Cep == null)
                return NotFound("CEP não encontrado.");

            return Ok(resultado);
        }
    }
}
