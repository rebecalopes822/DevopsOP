using OdontoPrevMLTrainer;
using Xunit;

namespace OdontoPrevAPI.Tests.IA
{
    public class ComplexidadeModelTests
    {
        [Fact]
        public void Predict_DeveRetornarResultadoValido()
        {
            var input = new ComplexidadeModel.ModelInput
            {
                TipoTratamento = "Clínico",
                Idade = 30,
                Sintomatico = "Sim"
            };

            var resultado = ComplexidadeModel.Predict(input);

            Assert.NotNull(resultado);
            Assert.False(string.IsNullOrEmpty(resultado.PredictedLabel));
        }
    }
}
