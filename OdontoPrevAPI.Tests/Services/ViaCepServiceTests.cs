using Moq;
using Moq.Protected;
using OdontoPrevAPI.DTOs;
using OdontoPrevAPI.Services;
using System.Net;
using System.Text.Json;
using Xunit;

namespace OdontoPrevAPI.Tests.Services
{
    public class ViaCepServiceTests
    {
        [Fact]
        public async Task ConsultarCepAsync_DeveRetornarEndereco_Valido()
        {
            // Arrange
            var cep = "01001000";
            var respostaEsperada = new ViaCepResponseDTO
            {
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP"
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(respostaEsperada))
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var service = new ViaCepService(httpClient);

            // Act
            var resultado = await service.ConsultarCepAsync(cep);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("01001-000", resultado.Cep);
            Assert.Equal("São Paulo", resultado.Localidade);
            Assert.Equal("SP", resultado.Uf);
        }
    }
}
