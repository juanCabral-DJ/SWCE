using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
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
    public class UnitTestCuponMontoFijoServices
    {
        public readonly ICuponMontoFijoServices cuponMontoFijoServices;
        private readonly Mock<IRepositorioCuponMontoFijo> mockCuponRepository;
        private readonly Mock<ILoggerBase<CuponMontoFijoService>> mockLogger;
        private readonly Mock<IConfiguration> mockConfiguration;

        public UnitTestCuponMontoFijoServices()
        {
            mockCuponRepository = new Mock<IRepositorioCuponMontoFijo>();
            mockLogger = new Mock<ILoggerBase<CuponMontoFijoService>>();
            mockConfiguration = new Mock<IConfiguration>();

            cuponMontoFijoServices = new CuponMontoFijoService(
                mockCuponRepository.Object,
                mockLogger.Object,
                mockConfiguration.Object);
        }

        [Fact]
        public async void CuponMontoFijoValidatorAdd_WhenMontoIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var cuponDto = new CreateCuponMontoFijoDto
            {
                Monto = 0,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            //Act
            string message = "El monto debe ser mayor que cero";
            var result = await cuponMontoFijoServices.Createasync(cuponDto);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorAdd_WhenFechaExpiracionIsPast_ShouldHaveValidationError()
        {
            //Arrange
            var cuponDto = new CreateCuponMontoFijoDto
            {
                Monto = 100,
                FechaExpiracion = DateTime.Now.AddDays(-1)
            };

            //Act
            string message = "La fecha de expiración debe ser futura";
            var result = await cuponMontoFijoServices.Createasync(cuponDto);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorAdd_WhenDataIsValid_ShouldReturnSuccess()
        {
            //Arrange
            var cuponDto = new CreateCuponMontoFijoDto
            {
                Monto = 100,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            mockCuponRepository.Setup(x => x.Createasync(It.IsAny<CuponMontoFijo>()))
                .ReturnsAsync(OperationResult.Success("Cupón creado exitosamente"));

            //Act
            string message = "Cupón creado exitosamente";
            var result = await cuponMontoFijoServices.Createasync(cuponDto);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorUpdate_WhenIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var cuponDto = new UpdateCuponMontoFijoDto
            {
                Id = 0,
                Monto = 100,
                FechaExpiracion = DateTime.Now.AddDays(1)
            };

            //Act
            string message = "El ID debe ser positivo";
            var result = await cuponMontoFijoServices.Updateasync(cuponDto);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorGetById_WhenIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            int id = 0;

            //Act
            string message = "El ID debe ser positivo";
            var result = await cuponMontoFijoServices.GetbyId(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorDisable_WhenIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            int id = 0;

            //Act
            string message = "El ID debe ser positivo";
            var result = await cuponMontoFijoServices.Disableasync(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorDisable_WhenCuponNotFound_ShouldHaveValidationError()
        {
            //Arrange
            int id = 999;
            mockCuponRepository.Setup(x => x.GetbyIdasync(id))
                .ReturnsAsync(OperationResult.Failure("Cupón no encontrado"));

            //Act
            string message = "Cupón no encontrado";
            var result = await cuponMontoFijoServices.Disableasync(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorDisable_WhenCuponIsWrongType_ShouldHaveValidationError()
        {
            //Arrange
            int id = 1;
            mockCuponRepository.Setup(x => x.GetbyIdasync(id))
                .ReturnsAsync(OperationResult.Success("", new object())); // Objeto incorrecto

            //Act
            string message = "Cupón inválido: tipo esperado CuponMontoFijo";
            var result = await cuponMontoFijoServices.Disableasync(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains(message, result.Message);
        }

        [Fact]
        public async void CuponMontoFijoValidatorGetAll_ShouldReturnSuccess()
        {
            //Arrange
            mockCuponRepository.Setup(x => x.GetAllasync(It.IsAny<Expression<Func<CuponMontoFijo, bool>>>()))
                .ReturnsAsync(OperationResult.Success("Cupones encontrados", new System.Collections.Generic.List<CuponMontoFijo>()));

            //Act
            string message = "Cupones encontrados";
            var result = await cuponMontoFijoServices.GetAllasync();

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}