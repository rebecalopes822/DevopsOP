using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Controllers;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;
using Xunit;

namespace OdontoPrevAPI.Tests.Controllers
{
    public class PacienteControllerTests
    {
        [Fact]
        public async Task Get_DeveRetornarOkComLista()
        {
            var options = new DbContextOptionsBuilder<OdontoPrevContext>()
                .UseInMemoryDatabase("PacienteControllerTestDb")
                .Options;

            using var context = new OdontoPrevContext(options);
            context.Pacientes.Add(new Paciente
            {
                Nome = "Paciente Teste",
                Email = "teste@email.com",
                Telefone = "11999999999"
            });
            await context.SaveChangesAsync();

            var controller = new PacienteController(context);

            var resultado = await controller.Get();

            var ok = Assert.IsType<OkObjectResult>(resultado.Result);
            var lista = Assert.IsAssignableFrom<IEnumerable<Paciente>>(ok.Value);
            Assert.Single(lista);
        }
    }
}
