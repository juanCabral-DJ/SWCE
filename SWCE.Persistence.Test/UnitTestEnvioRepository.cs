using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplication.Extension.Validators.EnvioValidator;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;
using Xunit;
using SWCE.Aplicatition.Interfaces.Repositories.EnvioModule;

namespace SWCE.Persistence.Test
{
    public class UnitTestEnvio
    {
        public readonly IEnvioRepository repositoryEnvio;

        public UnitTestEnvio()
        {
            var mockLogger = new Mock<ILoggerBase<EnvioEntity>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockEnvioValidator = new Mock<EnvioValidator>();
            var mockUpdateEnvioValidator = new Mock<UpdateEnvioValidator>();

            mockConfiguration.Setup(c => c["ConnectionStrings:E-CommerceConnection"])
                .Returns("Fake_Connection_String");

            this.repositoryEnvio = new EnvioRepository(
                mockLogger.Object,
                mockConfiguration.Object,
                mockEnvioValidator.Object,
                mockUpdateEnvioValidator.Object
            );
        }

        [Fact]
        public async void TestAddEnvio_ShouldReturnFailure_WhenEnvioIsNull()
        {
            // Arrange
            EnvioEntity envio = null!;

            // Act
            var result = await repositoryEnvio.CreateAsync(envio);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("error", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async void TestUpdateEnvio_ShouldReturnFailure_WhenEnvioIsNull()
        {
            // Arrange
            EnvioEntity envio = null!;

            // Act
            var result = await repositoryEnvio.UpdateAsync(envio!);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("error", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async void TestGetById_ShouldReturnFailure_WhenIdIsZero()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await repositoryEnvio.GetByIdAsync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("Ocurrio un error mientras se recuperaba e", result.Message);
        }

        [Fact]
        public async void TestGetByUserId_ShouldReturnFailure_WhenUserIdIsZero()
        {
            // Arrange
            int userId = 0;

            // Act
            var result = await repositoryEnvio.GetByUserId(userId);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("Ha ocurrido un error recuperando los envi", result.Message);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            var result = await repositoryEnvio.GetAllAsync();

            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("error", result.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
