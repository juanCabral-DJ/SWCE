using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;
using Xunit;

namespace SWCE.Persistence.Test
{
    public sealed class UnitTestCategoria
    {
        private readonly CategoriaRepository repositoryCategoria;

        public UnitTestCategoria()
        {
            var mockLogger = new Mock<ILoggerBase<Categoria>>();
            var mockConfiguration = new Mock<IConfiguration>();

            this.repositoryCategoria = new CategoriaRepository(
                mockConfiguration.Object,
                null!,
                null!,
                mockLogger.Object
            );
        }

        [Fact]
        public async Task TestCreateCategoria_ShouldReturnFailure_WhenCategoriaIsNull()
        {
            // Arrange
            Categoria categoria = null!;
            string message = "Error interno al agregar categoría";

            // Act
            var result = await repositoryCategoria.Createasync(categoria!);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task TestUpdateCategoria_ShouldReturnFailure_WhenCategoriaIsNull()
        {
            // Arrange
            Categoria categoria = null!;
            string message = "Error interno al actualizar categoría";

            // Act
            var result = await repositoryCategoria.Updateasync(categoria!);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task TestDisableCategoria_ShouldReturnFailure_WhenIdIsInvalid()
        {
            // Arrange
            int id = 0;
            string message = "Error interno al deshabilitar categoría";

            // Act
            var result = await repositoryCategoria.DisableAsync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task TestGetById_ShouldReturnFailure_WhenIdIsInvalid()
        {
            // Arrange
            int id = -1;
            string message = "Error interno al obtener categoría";

            // Act
            var result = await repositoryCategoria.GetbyIdasync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task TestGetAllAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            string message = "Error interno al obtener categorías";

            // Act
            //var result = await repositoryCategoria.GetAllasync();
            var result = await repositoryCategoria.GetAllasync(c => true);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}
