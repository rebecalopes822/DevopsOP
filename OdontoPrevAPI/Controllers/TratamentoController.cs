using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Dtos;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamentoController : ControllerBase
    {
        private readonly TratamentoRepository _repository;

        public TratamentoController(OdontoPrevContext context)
        {
            _repository = new TratamentoRepository(context);
        }

        /// <summary>
        /// Obtém todos os tratamentos cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamento>>> Get()
        {
            return Ok(await _repository.GetAll());
        }

        /// <summary>
        /// Obtém um tratamento pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamento>> Get(int id)
        {
            var tratamento = await _repository.GetById(id);
            if (tratamento == null)
                return NotFound($"Tratamento {id} não encontrado.");

            return Ok(tratamento);
        }

        /// <summary>
        /// Cadastra um novo tratamento.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TratamentoDTO tratamentoDto)
        {
            if (tratamentoDto == null)
                return BadRequest("Os dados do tratamento são obrigatórios.");

            var tratamento = new Tratamento
            {
                Descricao = tratamentoDto.Descricao,
                Tipo = tratamentoDto.Tipo,
                Custo = tratamentoDto.Custo
            };

            await _repository.Add(tratamento);
            return CreatedAtAction(nameof(Get), new { id = tratamento.Id }, tratamento);
        }

        /// <summary>
        /// Atualiza um tratamento existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TratamentoDTO tratamentoDto)
        {
            if (id != tratamentoDto.Id)
                return BadRequest("O ID na URL não corresponde ao do tratamento.");

            var tratamentoExistente = await _repository.GetById(id);
            if (tratamentoExistente == null)
                return NotFound($"Tratamento {id} não encontrado.");

            tratamentoExistente.Descricao = tratamentoDto.Descricao;
            tratamentoExistente.Tipo = tratamentoDto.Tipo;
            tratamentoExistente.Custo = tratamentoDto.Custo;

            await _repository.Update(tratamentoExistente);
            return NoContent();
        }

        /// <summary>
        /// Exclui um tratamento pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tratamento = await _repository.GetById(id);
            if (tratamento == null)
                return NotFound($"Tratamento {id} não encontrado.");

            await _repository.Delete(id);
            return NoContent();
        }
    }
}
