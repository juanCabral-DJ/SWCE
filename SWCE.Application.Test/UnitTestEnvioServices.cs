using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplication.DTOs.Envio;
using SWCE.Aplication.Interfaces.Services;
using SWCE.Aplication.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;

namespace SWCE.Application.Test
{
    public class UnitTestEnvioServices
    {
        private readonly IEnvioServices envioServices;
        private readonly Mock<IEnvioRepository> mockRepository;

        public UnitTestEnvioServices()
        {
            mockRepository = new Mock<IEnvioRepository>();
            var mockLogger = new Mock<ILoggerBase<EnvioEntity>>();
            var mockConfiguration = new Mock<IConfiguration>();

            envioServices = new EnvioServices(
                mockRepository.Object,
                mockLogger.Object,
                mockConfiguration.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_WhenValidDto()
        {
            // Arrange
            var dto = new CreateEnvioDto
            {
                UsuarioId = 1,
                Estado = "Pendiente",
                Costo = 100.50m,
                TipoEnvio = "Express",
                FechaPedido = DateTime.UtcNow
            };

            mockRepository.Setup(r => r.CreateAsync(It.IsAny<EnvioEntity>()))
                .ReturnsAsync(OperationResult.Success("Creado con éxito"));

            // Act
            var result = await envioServices.CreateAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Creado con éxito", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenValidDto()
        {
            // Arrange
            var dto = new UpdateEnvioDto
            {
                Id = 1,
                Estado = "Enviado"
            };

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<EnvioEntity>()))
                .ReturnsAsync(OperationResult.Success("Actualizado con éxito"));

            // Act
            var result = await envioServices.UpdateAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Actualizado con éxito", result.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccess_WhenRepositoryReturnsData()
        {
            // Arrange
            int id = 1;
            var envio = new EnvioEntity { Id = id, Estado = "Pendiente", TipoEnvio = "Normal" };

            mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(OperationResult.Success("OK", envio));

            // Act
            var result = await envioServices.GetByIdAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Envio retrieved successfully.", result.Message);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenRepositoryReturnsList()
        {
            // Arrange
            var list = new List<EnvioEntity>
            {
                new EnvioEntity { Id = 1, Estado = "Pendiente", TipoEnvio = "Normal" },
                new EnvioEntity { Id = 2, Estado = "Enviado", TipoEnvio = "Express" }
            };

            mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(OperationResult.Success("OK", list));

            // Act
            var result = await envioServices.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Envio recuperado satisfactoriamente.", result.Message);
            Assert.IsAssignableFrom<IEnumerable<EnvioEntity>>(result.Data);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFailure_WhenRepositoryThrows()
        {
            // Arrange
            var dto = new CreateEnvioDto
            {
                UsuarioId = 1,
                Estado = "Pendiente",
                Costo = 99.99m,
                TipoEnvio = "Normal",
                FechaPedido = DateTime.UtcNow
            };

            mockRepository.Setup(r => r.CreateAsync(It.IsAny<EnvioEntity>()))
                .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await envioServices.CreateAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while creating Envio", result.Message);
        }
    }
}
