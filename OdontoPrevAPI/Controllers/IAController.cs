using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.DTOs;
using OdontoPrevMLTrainer;

namespace OdontoPrevAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IAController : ControllerBase
    {
        /// <summary>
        /// Realiza a previsão da complexidade do tratamento.
        /// </summary>
        [HttpPost("prever-complexidade")]
        public IActionResult PreverComplexidade([FromBody] ComplexidadeInputDTO input)
        {
            if (input == null)
                return BadRequest("Dados inválidos.");

            var tratamentosValidos = new[] { "Clínico", "Ortodontia", "Cirúrgico" };
            var sintomasValidos = new[] { "Sim", "Não" };

            if (!tratamentosValidos.Contains(input.TipoTratamento))
                return BadRequest("Tipo de tratamento inválido. Válidos: Clínico, Ortodontia, Cirúrgico.");

            if (!sintomasValidos.Contains(input.Sintomatico))
                return BadRequest("Sintomático inválido. Válidos: Sim ou Não.");

            if (input.Idade < 0 || input.Idade > 100)
                return BadRequest("Idade inválida. Informe entre 0 e 100.");

            var modelInput = new ComplexidadeModel.ModelInput
            {
                TipoTratamento = input.TipoTratamento,
                Idade = input.Idade,
                Sintomatico = input.Sintomatico
            };

            var resultado = ComplexidadeModel.Predict(modelInput);

            return Ok(new
            {
                ComplexidadePrevista = resultado.PredictedLabel
            });
        }
    }
}