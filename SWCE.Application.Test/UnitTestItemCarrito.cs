using Moq;
using FluentValidation;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Services;
using SWCE.Domain.Entities;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Domain.Repository;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using SWCE.Application.Services.Base;
using Xunit;
using FluentValidation.Results;

namespace SWCE.Application.Test
{
    public class UnitTestItemCarrito
    {
        private readonly Mock<IItemCarritoRepository> _mockItemRepo;
        private readonly Mock<IRepositorioProducto> _mockProductRepo;
        private readonly Mock<ICarritoService> _mockCarritoService;
        private readonly Mock<IValidator<AddItemCarritoDto>> _mockAddValidator;
        private readonly Mock<IValidator<UpdateItemCantidadDto>> _mockUpdateValidator;
        private readonly Mock<ILoggerBase<ServiceBase<ItemCarrito>>> _mockLogger;
        private readonly ItemCarritoService _service;

        public UnitTestItemCarrito()
        {
            _mockItemRepo = new Mock<IItemCarritoRepository>();
            _mockProductRepo = new Mock<IRepositorioProducto>();
            _mockCarritoService = new Mock<ICarritoService>();
            _mockAddValidator = new Mock<IValidator<AddItemCarritoDto>>();
            _mockUpdateValidator = new Mock<IValidator<UpdateItemCantidadDto>>();
            _mockLogger = new Mock<ILoggerBase<ServiceBase<ItemCarrito>>>();

            _service = new ItemCarritoService(
                _mockItemRepo.Object,
                _mockLogger.Object,
                _mockAddValidator.Object,
                _mockUpdateValidator.Object,
                _mockProductRepo.Object,
                _mockCarritoService.Object
            );
        }


        [Fact]
        public async Task AddItemToCarritoAsync_ShouldSucceed_WhenDtoIsValidAndProductExists()
        {
            // Arrange
            var dto = new AddItemCarritoDto { CarritoId = 1, IdProducto = 1, Cantidad = 2 };
            var product = new Producto { Id = 1, Nombre = "Test Product", Precio = 50.00m };
            _mockAddValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mockProductRepo.Setup(r => r.GetbyIdasync(dto.IdProducto)).ReturnsAsync(OperationResult.Success("Ok", product));
            _mockItemRepo.Setup(r => r.AddItemAsync(It.IsAny<ItemCarrito>())).ReturnsAsync(OperationResult.Success("Item añadido."));

            // Act
            var result = await _service.AddItemToCarritoAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockCarritoService.Verify(s => s.UpdateCarritoTotal(dto.CarritoId), Times.Once);
        }

        [Fact]
        public async Task AddItemToCarritoAsync_ShouldFail_WhenProductNotFound()
        {
            // Arrange
            var dto = new AddItemCarritoDto { CarritoId = 1, IdProducto = 99, Cantidad = 1 };
            _mockAddValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mockProductRepo.Setup(r => r.GetbyIdasync(dto.IdProducto)).ReturnsAsync(OperationResult.Failure("Producto no encontrado."));

            // Act
            var result = await _service.AddItemToCarritoAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Producto no encontrado.", result.Message);
        }

        [Fact]
        public async Task UpdateItemCantidadAsync_ShouldSucceed_WhenItemAndProductExist()
        {
            // Arrange
            var dto = new UpdateItemCantidadDto { Id = 1, NewCantidad = 5 };
            var existingItem = new ItemCarrito { Id = 1, CarritoId = 10, IdProducto = 2 };
            var product = new Producto { Id = 2, Precio = 25.00m };
            _mockUpdateValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mockItemRepo.Setup(r => r.GetbyIdasync(dto.Id)).ReturnsAsync(OperationResult.Success("Ok", existingItem));
            _mockProductRepo.Setup(r => r.GetbyIdasync(existingItem.IdProducto)).ReturnsAsync(OperationResult.Success("Ok", product));
            _mockItemRepo.Setup(r => r.Updateasync(It.IsAny<ItemCarrito>())).ReturnsAsync(OperationResult.Success("Actualizado"));

            // Act
            var result = await _service.UpdateItemCantidadAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockItemRepo.Verify(r => r.Updateasync(It.Is<ItemCarrito>(item => item.Cantidad == dto.NewCantidad)), Times.Once);
            _mockCarritoService.Verify(s => s.UpdateCarritoTotal(existingItem.CarritoId), Times.Once);
        }

        [Fact]
        public async Task RemoveItemFromCarritoAsync_ShouldSucceed_WhenItemExists()
        {
            // Arrange
            int itemId = 1;
            var itemInDb = new ItemCarrito { Id = itemId, CarritoId = 100 };
            _mockItemRepo.Setup(r => r.GetbyIdasync(itemId)).ReturnsAsync(OperationResult.Success("Ok", itemInDb));
            _mockItemRepo.Setup(r => r.RemoveItemAsync(itemId)).ReturnsAsync(OperationResult.Success("Eliminado"));

            // Act
            var result = await _service.RemoveItemFromCarritoAsync(itemId);

            // Assert
            Assert.True(result.IsSuccess);
            _mockCarritoService.Verify(s => s.UpdateCarritoTotal(itemInDb.CarritoId), Times.Once);
        }


        [Fact]
        public async Task AddItemToCarritoAsync_ShouldFail_WhenDtoIsInvalid()
        {
            // Arrange
            var dto = new AddItemCarritoDto { CarritoId = 0 }; 
            var validationFail = new ValidationResult(new[] { new ValidationFailure("CarritoId", "Error de validación") });
            _mockAddValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationFail);

            // Act
            var result = await _service.AddItemToCarritoAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error de validación", result.Message);
            _mockProductRepo.Verify(r => r.GetbyIdasync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task UpdateItemCantidadAsync_ShouldFail_WhenItemNotFound()
        {
            // Arrange
            var dto = new UpdateItemCantidadDto { Id = 999, NewCantidad = 3 };
            _mockUpdateValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            _mockItemRepo.Setup(r => r.GetbyIdasync(dto.Id)).ReturnsAsync(OperationResult.Failure("Item no encontrado"));

            // Act
            var result = await _service.UpdateItemCantidadAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"Item with ID {dto.Id} not found.", result.Message);
            _mockItemRepo.Verify(r => r.Updateasync(It.IsAny<ItemCarrito>()), Times.Never);
        }

        [Fact]
        public async Task RemoveItemFromCarritoAsync_ShouldFail_WhenItemIdIsInvalid()
        {
            // Arrange
            int itemId = 0; 

            // Act
            var result = await _service.RemoveItemFromCarritoAsync(itemId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The item ID must be greater than zero.", result.Message);
            _mockItemRepo.Verify(r => r.RemoveItemAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task RemoveItemFromCarritoAsync_ShouldFail_WhenItemNotFound()
        {
            // Arrange
            int itemId = 999;
            _mockItemRepo.Setup(r => r.GetbyIdasync(itemId)).ReturnsAsync(OperationResult.Failure("No encontrado"));

            // Act
            var result = await _service.RemoveItemFromCarritoAsync(itemId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal($"Item with ID {itemId} not found.", result.Message);
            _mockItemRepo.Verify(r => r.RemoveItemAsync(itemId), Times.Never);
        }
    }
}