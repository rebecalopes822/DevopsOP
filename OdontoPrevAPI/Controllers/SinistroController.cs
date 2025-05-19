using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories;
using OdontoPrevAPI.Data;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinistroController : ControllerBase
    {
        private readonly SinistroRepository _repositorySinistro;
        private readonly PacienteRepository _repositoryPaciente;
        private readonly TratamentoRepository _repositoryTratamento;

        public SinistroController(OdontoPrevContext context)
        {
            _repositorySinistro = new SinistroRepository(context);
            _repositoryPaciente = new PacienteRepository(context);
            _repositoryTratamento = new TratamentoRepository(context);
        }

        /// <summary>
        /// Obtém todos os sinistros cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sinistro>>> Get()
        {
            return Ok(await _repositorySinistro.GetAll());
        }

        /// <summary>
        /// Obtém um sinistro pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Sinistro>> Get(int id)
        {
            var sinistro = await _repositorySinistro.GetById(id);
            if (sinistro == null)
                return NotFound($"Sinistro {id} não encontrado.");

            return Ok(sinistro);
        }

        /// <summary>
        /// Cadastra um novo sinistro.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Sinistro sinistro)
        {
            if (sinistro == null)
                return BadRequest("Os dados do sinistro são obrigatórios.");

            if (await _repositoryPaciente.GetById(sinistro.PacienteId) == null)
                return NotFound($"Paciente {sinistro.PacienteId} não encontrado.");

            if (await _repositoryTratamento.GetById(sinistro.TratamentoId) == null)
                return NotFound($"Tratamento {sinistro.TratamentoId} não encontrado.");

            await _repositorySinistro.Add(sinistro);
            return CreatedAtAction(nameof(Get), new { id = sinistro.Id }, sinistro);
        }

        /// <summary>
        /// Atualiza um sinistro existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Sinistro sinistro)
        {
            if (id != sinistro.Id)
                return BadRequest("O ID na URL não corresponde ao do sinistro.");

            var sinistroExistente = await _repositorySinistro.GetById(id);
            if (sinistroExistente == null)
                return NotFound($"Sinistro {id} não encontrado.");

            await _repositorySinistro.Update(sinistro);
            return NoContent();
        }

        /// <summary>
        /// Exclui um sinistro pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sinistro = await _repositorySinistro.GetById(id);
            if (sinistro == null)
                return NotFound($"Sinistro {id} não encontrado.");

            await _repositorySinistro.Delete(id);
            return NoContent();
        }
    }
}
