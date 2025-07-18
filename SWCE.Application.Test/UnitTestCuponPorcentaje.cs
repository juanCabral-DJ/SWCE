/*using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System;
using System.Linq.Expressions;
using Xunit;

namespace SWCE.Application.Test
{
    public class UnitTestCuponPorcentajeServices
    {
        public readonly ICuponPorcentajeServices cuponPorcentajeServices;
        private readonly Mock<IRepositorioCuponPorcentaje> mockCuponRepository;
        private readonly Mock<ILoggerBase<CuponPorcentajeService>> mockLogger;
        private readonly Mock<IConfiguration> mockConfiguration;

        public UnitTestCuponPorcentajeServices()
        {
            mockCuponRepository = new Mock<IRepositorioCuponPorcentaje>();
            mockLogger = new Mock<ILoggerBase<CuponPorcentajeService>>();
            mockConfiguration = new Mock<IConfiguration>();

            cuponPorcentajeServices = new CuponPorcentajeService(
                mockCuponRepository.Object,
                mockLogger.Object,
                mockConfiguration.Object);
        }

        [Fact]
        public async void CuponPorcentajeValidatorAdd_WhenPorcentajeIsZero_ShouldHaveValidationError()
        {
            // Arrange
            var cuponDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = 0,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await cuponPorcentajeServices.Createasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El porcentaje debe ser mayor que cero", result.Message);
            mockCuponRepository.Verify(x => x.Createasync(It.IsAny<CuponPorcentaje>()), Times.Never);
        }

        [Fact]
        public async void CuponPorcentajeValidatorAdd_WhenPorcentajeIsNegative_ShouldHaveValidationError()
        {
            // Arrange
            var cuponDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = -5,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await cuponPorcentajeServices.Createasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El porcentaje no puede ser negativo", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorAdd_WhenPorcentajeOver100_ShouldHaveValidationError()
        {
            // Arrange
            var cuponDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = 150,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await cuponPorcentajeServices.Createasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El porcentaje no puede ser mayor a 100", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorAdd_WhenFechaExpiracionIsPast_ShouldHaveValidationError()
        {
            // Arrange
            var cuponDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = 20,
                FechaExpiracion = DateTime.Now.AddDays(-1)
            };

            // Act
            var result = await cuponPorcentajeServices.Createasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("La fecha de expiración debe ser futura", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorAdd_WhenDataIsValid_ShouldReturnSuccess()
        {
            // Arrange
            var cuponDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = 15,
                FechaExpiracion = DateTime.Now.AddDays(10)
            };

            mockCuponRepository.Setup(x => x.Createasync(It.IsAny<CuponPorcentaje>()))
                .ReturnsAsync(OperationResult.Success("Cupón creado exitosamente"));

            // Act
            var result = await cuponPorcentajeServices.Createasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            mockCuponRepository.Verify(x => x.Createasync(It.IsAny<CuponPorcentaje>()), Times.Once);
        }

        [Fact]
        public async void CuponPorcentajeValidatorUpdate_WhenIdIsZero_ShouldHaveValidationError()
        {
            // Arrange
            var cuponDto = new UpdateCuponPorcentajeDto
            {
                Id = 0,
                Porcentaje = 10,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await cuponPorcentajeServices.Updateasync(cuponDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorGetById_WhenIdIsZero_ShouldHaveValidationError()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await cuponPorcentajeServices.GetbyId(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorDisable_WhenIdIsZero_ShouldHaveValidationError()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await cuponPorcentajeServices.Disableasync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorDisable_WhenCuponNotFound_ShouldReturnFailure()
        {
            // Arrange
            int id = 999;
            mockCuponRepository.Setup(x => x.GetbyIdasync(id))
                .ReturnsAsync(OperationResult.Failure("Cupón no encontrado"));

            // Act
            var result = await cuponPorcentajeServices.Disableasync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("no encontrado", result.Message);
        }

        [Fact]
        public async void CuponPorcentajeValidatorGetAll_ShouldReturnSuccess()
        {
            // Arrange
            mockCuponRepository.Setup(x => x.GetAllasync(It.IsAny<Expression<Func<CuponPorcentaje, bool>>>()))
                .ReturnsAsync(OperationResult.Success("Cupones encontrados", new System.Collections.Generic.List<CuponPorcentaje>()));

            // Act
            var result = await cuponPorcentajeServices.GetAllasync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Contains("encontrados", result.Message);
        }
    }
}*/

using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Application.Interfaces.Repositories.AdministracionModule;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using Xunit;

namespace SWCE.Application.Test
{
    public class UnitTestCuponPorcentajeServices
    {
        private readonly ICuponPorcentajeServices _service;
        private readonly Mock<IRepositorioCuponPorcentaje> _mockRepo = new();
        private readonly Mock<ILoggerBase<CuponPorcentajeService>> _mockLogger = new();

        public UnitTestCuponPorcentajeServices()
        {
            _service = new CuponPorcentajeService(
                _mockRepo.Object,
                _mockLogger.Object,
                new Mock<IConfiguration>().Object);
        }

        [Fact]
        public async void Create_WithInvalidPorcentaje_ShouldFail()
        {
            // Arrange
            var invalidDto = new CreateCuponPorcentajeDto { Porcentaje = -5 };

            // Act
            var result = await _service.Createasync(invalidDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("porcentaje", result.Message.ToLower());
        }

        [Fact]
        public async void Create_WithValidData_ShouldSucceed()
        {
            // Arrange
            var validDto = new CreateCuponPorcentajeDto
            {
                Porcentaje = 15,
                FechaExpiracion = DateTime.Now.AddDays(10)
            };
            _mockRepo.Setup(x => x.Createasync(It.IsAny<CuponPorcentaje>()))
                .ReturnsAsync(OperationResult.Success("Creado"));

            // Act
            var result = await _service.Createasync(validDto);

            // Assert
            Assert.True(result.IsSuccess);
            _mockRepo.Verify(x => x.Createasync(It.IsAny<CuponPorcentaje>()), Times.Once);
        }

        [Fact]
        public async void Update_WithInvalidId_ShouldFail()
        {
            // Arrange
            var invalidDto = new UpdateCuponPorcentajeDto { Id = 0 };

            // Act
            var result = await _service.Updateasync(invalidDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("ID", result.Message);
        }

        [Fact]
        public async void Disable_WithNonexistentId_ShouldFail()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetbyIdasync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult.Failure("No encontrado"));

            // Act
            var result = await _service.Disableasync(999);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async void GetById_WithValidId_ShouldReturnCupon()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetbyIdasync(1))
                .ReturnsAsync(OperationResult.Success("Ok", new CuponPorcentaje()));

            // Act
            var result = await _service.GetbyId(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.IsType<CuponPorcentaje>(result.Data);
        }
    }
}