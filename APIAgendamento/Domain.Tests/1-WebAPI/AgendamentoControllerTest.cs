using API.Controllers;
using Application.Interfaces;
using Moq;

namespace Domain.Tests._1_WebAPI
{
    public class AgendamentoControllerTest
    {
        private readonly Mock<IAgendamentoService> _mockService;
        private readonly AgendamentoController _controller;
        private readonly Mock<IAgendamentoService> _AppService = new();

        public AgendamentoControllerTest()
        {
            _mockService = new Mock<IAgendamentoService>();
            _controller = new AgendamentoController(_AppService.Object);
        }

        //[Trait("Categoria", "PedidoController")]
        //[Fact(DisplayName = "BuscarListaPedidos OkResult")]
        //public async Task GetPedidos_ReturnsOkResult_BuscarListaPedidos()
        //{
        //    // Arrange
        //    _AppService.Setup(service => service.GetPedidos())
        //        .ReturnsAsync(new List<Pedido> { new(), new() });

        //    // Act
        //    var result = await _controller.GetPedidos();

        //    // Assert
        //    Assert.NotNull(result);
        //}

    }
}
