using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories;
using OdontoPrevAPI.Data;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteRepository _repositoryPaciente;
        private readonly SinistroRepository _repositorySinistro;

        public PacienteController(OdontoPrevContext context)
        {
            _repositoryPaciente = new PacienteRepository(context);
            _repositorySinistro = new SinistroRepository(context);
        }

        /// <summary>
        /// Obtém todos os pacientes cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> Get()
        {
            return Ok(await _repositoryPaciente.GetAll());
        }

        /// <summary>
        /// Obtém um paciente pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> Get(int id)
        {
            var paciente = await _repositoryPaciente.GetById(id);
            if (paciente == null)
                return NotFound("Paciente não encontrado.");

            return Ok(paciente);
        }

        /// <summary>
        /// Cadastra um novo paciente.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Paciente paciente)
        {
            if (paciente == null || string.IsNullOrWhiteSpace(paciente.Nome) || string.IsNullOrWhiteSpace(paciente.Email))
                return BadRequest("Nome e Email são obrigatórios.");

            await _repositoryPaciente.Add(paciente);
            return CreatedAtAction(nameof(Get), new { id = paciente.Id }, paciente);
        }

        /// <summary>
        /// Atualiza um paciente existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest("IDs não coincidem.");

            var pacienteExistente = await _repositoryPaciente.GetById(id);
            if (pacienteExistente == null)
                return NotFound("Paciente não encontrado.");

            await _repositoryPaciente.Update(paciente);
            return NoContent();
        }

        /// <summary>
        /// Exclui um paciente pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paciente = await _repositoryPaciente.GetById(id);
            if (paciente == null)
                return NotFound("Paciente não encontrado.");

            var possuiSinistros = await _repositorySinistro.ExisteSinistroParaPaciente(id);
            if (possuiSinistros)
                return BadRequest("Não é possível excluir. O paciente possui sinistros vinculados.");

            await _repositoryPaciente.Delete(id);
            return NoContent();
        }
    }
}
