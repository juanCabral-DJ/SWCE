using Microsoft.EntityFrameworkCore;
using Moq;
using SWCE.Application.Extension.Validators.ProductoValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;

namespace SWCE.Persistence.Test
{
    public class UnitTestProducto
    {
        private readonly ProductoRepository _repository;

        public UnitTestProducto()
        {
            var options = new DbContextOptionsBuilder<E_commerceContext>()
                .UseInMemoryDatabase(databaseName: "Test_DB_Producto")
                .Options;

            var mockLogger = new Mock<ILoggerBase<Producto>>();
            var context = new E_commerceContext(options);

            var createValidator = new CreateProductoValidator();
            var updateValidator = new UpdateProductoValidator();

            _repository = new ProductoRepository(context, mockLogger.Object, createValidator, updateValidator);
        }

        [Fact]
        public async Task CreateProducto_ShouldReturnFailure_WhenProductoIsNull()
        {
            // Arrange
            Producto producto = null!;
            string message = "Entidad no válida para crear un producto.";

            // Act
            var result = await _repository.Createasync(producto!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task UpdateProducto_ShouldReturnFailure_WhenProductoIsNull()
        {
            // Arrange
            Producto producto = null!;
            string message = "El producto no puede ser nulo para la actualización.";

            // Act
            var result = await _repository.Updateasync(producto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task DisableProducto_ShouldReturnFailure_WhenProductoDoesNotExist()
        {
            // Arrange
            int id = 98945;
            string message = "Producto no encontrado.";

            // Act
            var result = await _repository.DisableAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnFailure_WhenIdIsZero()
        {
            // Arrange
            int id = 0;
            string message = "Id inválido para buscar el producto.";

            // Act
            var result = await _repository.GetbyIdasync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetAll_ShouldReturnSuccess_WhenNoError()
        {
            // Arrange
            string message = "Productos obtenidos correctamente.";

            // Act
            var result = await _repository.GetAllasync(p => (bool)!p.IsDeleted!);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}
