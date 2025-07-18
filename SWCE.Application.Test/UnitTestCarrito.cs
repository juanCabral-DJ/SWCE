using Moq;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Application.Interfaces.Repositories;
using SWCE.Application.Interfaces.Repositories.CarritoModule;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Test
{
    public class UnitTestCarrito
    {
        private readonly Mock<ICarritoRepository> _mockCarritoRepository;
        private readonly Mock<IItemCarritoRepository> _mockItemCarritoRepository;
        private readonly Mock<ILoggerBase<CarritoService>> _mockLogger;
        private readonly CarritoService _carritoService;

        public UnitTestCarrito()
        {
            _mockCarritoRepository = new Mock<ICarritoRepository>();
            _mockItemCarritoRepository = new Mock<IItemCarritoRepository>();
            _mockLogger = new Mock<ILoggerBase<CarritoService>>();

            var createValidator = new CreateCarritoValidator();
            var updateValidator = new UpdateCarritoValidator();

            _carritoService = new CarritoService(
                _mockCarritoRepository.Object,
                _mockItemCarritoRepository.Object,
                _mockLogger.Object,
                createValidator,
                updateValidator
            );
        }

        [Fact]
        public async Task CreateAsync_Should_Succeed_When_User_Has_No_Cart()
        {
            // Arrange
            var createDto = new CreateCarritoDto { ID_Usuario = 1 };

            _mockCarritoRepository.Setup(repo => repo.GetByUserIdAsync(createDto.ID_Usuario))
                .ReturnsAsync(OperationResult.Failure("No carrito found."));

            _mockCarritoRepository.Setup(repo => repo.Createasync(It.IsAny<CreateCarritoDto>()))
                .ReturnsAsync(OperationResult.Success("Carrito creado exitosamente."));

            // Act
            var result = await _carritoService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Carrito creado exitosamente.", result.Message);

            _mockCarritoRepository.Verify(repo => repo.Createasync(It.IsAny<CreateCarritoDto>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_Should_Fail_When_User_Already_Has_Cart()
        {
            // Arrange
            var createDto = new CreateCarritoDto { ID_Usuario = 1 };
            var existingCarrito = new GetCarritoDto { Id = 99, ID_Usuario = 1 };

            _mockCarritoRepository.Setup(repo => repo.GetByUserIdAsync(createDto.ID_Usuario))
                .ReturnsAsync(OperationResult.Success("Carrito found.", existingCarrito));

            // Act
            var result = await _carritoService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("The user already has an active carrito.", result.Message);

            _mockCarritoRepository.Verify(repo => repo.Createasync(It.IsAny<CreateCarritoDto>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Carrito_With_Items_When_Found()
        {
            // Arrange
            int carritoId = 1;
            var carritoDto = new GetCarritoDto { Id = carritoId, ID_Usuario = 1, Productos = new List<GetItemCarritoDto>() };
            var itemsList = new List<GetItemCarritoDto> { new GetItemCarritoDto { Id = 101, NombreProducto = "Laptop" } };

            _mockCarritoRepository.Setup(repo => repo.GetbyIdasync(carritoId))
                .ReturnsAsync(OperationResult.Success("Carrito encontrado.", carritoDto));

            _mockItemCarritoRepository.Setup(repo => repo.GetItemsByCarritoIdAsync(carritoId))
                .ReturnsAsync(itemsList);

            // Act
            var result = await _carritoService.GetByIdAsync(carritoId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var data = result.Data as GetCarritoDto;
            Assert.NotNull(data);
            Assert.Equal(carritoId, data.Id);
            Assert.Single(data.Productos); 
            Assert.Equal("Laptop", data.Productos.First().NombreProducto);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Carrito_Without_Items_When_No_Items_Found()
        {
            // Arrange
            int carritoId = 1;
            var carritoDto = new GetCarritoDto { Id = carritoId, ID_Usuario = 1, Productos = new List<GetItemCarritoDto>() };

            _mockCarritoRepository.Setup(repo => repo.GetbyIdasync(carritoId))
                                  .ReturnsAsync(OperationResult.Success("Carrito encontrado.", carritoDto));

            _mockItemCarritoRepository.Setup(repo => repo.GetItemsByCarritoIdAsync(carritoId))
                                      .ReturnsAsync(new List<GetItemCarritoDto>()); 

            // Act
            var result = await _carritoService.GetByIdAsync(carritoId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            var data = result.Data as GetCarritoDto;
            Assert.NotNull(data);
            Assert.Equal(carritoId, data.Id);
            Assert.Empty(data.Productos); 
            _mockCarritoRepository.Verify(repo => repo.GetbyIdasync(carritoId), Times.Once);
            _mockItemCarritoRepository.Verify(repo => repo.GetItemsByCarritoIdAsync(carritoId), Times.Once);
            _mockLogger.Verify(log => log.LogInformation("Carrito obtenido exitosamente con items."), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Fail_When_Id_Is_Invalid()
        {
            // Arrange
            int invalidId = 0;

            // Act
            var result = await _carritoService.GetByIdAsync(invalidId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del carrito debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task DisableAsync_Should_Succeed_When_CarritoExists_And_Repo_Succeeds()
        {
            // Arrange
            var disableDto = new DisableCarritoDto { Id = 1 };
            _mockCarritoRepository.Setup(repo => repo.DeleteAsync(It.Is<DisableCarritoDto>(d => d.Id == disableDto.Id)))
                                  .ReturnsAsync(OperationResult.Success("Carrito eliminado exitosamente."));

            // Act
            var result = await _carritoService.DisableAsync(disableDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("Carrito eliminado exitosamente.", result.Message);
            _mockCarritoRepository.Verify(repo => repo.DeleteAsync(It.IsAny<DisableCarritoDto>()), Times.Once);
            _mockLogger.Verify(log => log.LogInformation("Carrito deshabilitado exitosamente."), Times.Once);
        }

        [Fact]
        public async Task DisableAsync_Should_Fail_When_Id_Is_Invalid()
        {
            // Arrange
            var invalidDto = new DisableCarritoDto { Id = 0 }; 

            // Act
            var result = await _carritoService.DisableAsync(invalidDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del carrito debe ser mayor que cero.", result.Message);
            _mockCarritoRepository.Verify(repo => repo.DeleteAsync(It.IsAny<DisableCarritoDto>()), Times.Never);
            _mockLogger.Verify(log => log.LogError("ID de carrito inválido para deshabilitar. Debe ser mayor que cero."), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_EmptyList_When_No_Carritos_Exist()
        {
            // Arrange
            _mockCarritoRepository.Setup(repo => repo.GetAllasync())
                                  .ReturnsAsync(OperationResult.Success("No carritos found.", new List<GetCarritoDto>())); 

            // Act
            var result = await _carritoService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var returnedCarritos = Assert.IsAssignableFrom<IEnumerable<GetCarritoDto>>(result.Data);
            Assert.Empty(returnedCarritos);
            _mockCarritoRepository.Verify(repo => repo.GetAllasync(), Times.Once);
            _mockItemCarritoRepository.Verify(repo => repo.GetItemsByCarritoIdAsync(It.IsAny<int>()), Times.Never); 
            _mockLogger.Verify(log => log.LogInformation("Todos los carritos obtenidos exitosamente con sus items."), Times.Once);
        }

        [Fact]
        public async Task GetActiveCarritoByUserIdAsync_Should_Fail_When_UserId_Is_Invalid()
        {
            // Arrange
            int invalidUserId = 0;

            // Act
            var result = await _carritoService.GetActiveCarritoByUserIdAsync(invalidUserId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del usuario debe ser mayor que cero.", result.Message);
            _mockCarritoRepository.Verify(repo => repo.GetByUserIdAsync(It.IsAny<int>()), Times.Never);
            _mockItemCarritoRepository.Verify(repo => repo.GetItemsByCarritoIdAsync(It.IsAny<int>()), Times.Never);
            _mockLogger.Verify(log => log.LogError("ID de usuario inválido para buscar carrito activo. Debe ser mayor que cero."), Times.Once);
        }

        [Fact]
        public async Task GetActiveCarritoByUserIdAsync_Should_Fail_When_No_Active_Carrito_Found()
        {
            // Arrange
            int userId = 1;
            _mockCarritoRepository.Setup(repo => repo.GetByUserIdAsync(userId))
                                  .ReturnsAsync(OperationResult.Failure("No carrito found."));

            // Act
            var result = await _carritoService.GetActiveCarritoByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("No se encontró un carrito activo para el usuario.", result.Message);
            _mockCarritoRepository.Verify(repo => repo.GetByUserIdAsync(userId), Times.Once);
            _mockItemCarritoRepository.Verify(repo => repo.GetItemsByCarritoIdAsync(It.IsAny<int>()), Times.Never);
            _mockLogger.Verify(log => log.LogInformation("No se encontró carrito activo para el usuario."), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Fail_When_Dto_Is_Invalid()
        {
            // Arrange

            var invalidDto = new UpdateCarritoDto { Id = 1, ID_Usuario = 10, Total = -50.0m, IsDeleted = false };

            // Act
            var result = await _carritoService.UpdateAsync(invalidDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El total no puede ser negativo.", result.Message);
            _mockCarritoRepository.Verify(repo => repo.Updateasync(It.IsAny<UpdateCarritoDto>()), Times.Never);
            _mockLogger.Verify(log => log.LogError("Fallo en la validación al actualizar el carrito."), Times.Once);
        }


    }
}
