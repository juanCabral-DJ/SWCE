using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Extension.Validators.CarritoValidator;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Repositories;
using SWCE.Domain.Base;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using SWCE.Domain.Entities;

namespace SWCE.Persistence.Test
{
    public class UnitTestCarrito
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILoggerBase<Carrito>> _mockLogger;
        private readonly CarritoRepository _carritoRepository;

        public UnitTestCarrito()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILoggerBase<Carrito>>();

            _mockConfiguration.Setup(config => config["ConnectionStrings:E-commerceConnection"])
                .Returns("a_dummy_connection_string");

            _carritoRepository = new CarritoRepository(
                _mockConfiguration.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task Createasync_Should_Fail_When_UserId_Is_Invalid()
        {
            // Arrange

            var invalidDto = new CreateCarritoDto { ID_Usuario = 0 };

            string expectedErrorMessage = "El Id del usuario debe ser mayor que cero.";

            // Act
            var result = await _carritoRepository.Createasync(invalidDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Message);
        }

        [Fact]
        public async Task Updateasync_Should_Fail_When_Total_Is_Negative()
        {
            // Arrange

            var invalidDto = new UpdateCarritoDto { Id = 1, ID_Usuario = 1, Total = -50.0m };

            string expectedErrorMessage = "El total no puede ser negativo.";

            // Act
            var result = await _carritoRepository.Updateasync(invalidDto);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Message);
        }


        [Fact]
        public async Task Updateasync_Should_Fail_When_Id_Is_Zero()
        {
            // Arrange
            var invalidDto = new UpdateCarritoDto { Id = 0, ID_Usuario = 1, Total = 50.0m, IsDeleted = false };
            string expectedErrorMessage = "El Id del carrito debe ser mayor que cero.";


            // Act
            var result = await _carritoRepository.Updateasync(invalidDto); 

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Message);

            // Assert
           
            _mockLogger.Verify(log => log.LogError("Falló la validación al actualizar el carrito"), Times.Once);
        }

        [Fact]
        public async Task Updateasync_Should_Fail_When_Id_Is_Negative()
        {
            // Arrange
            var invalidDto = new UpdateCarritoDto { Id = -1, ID_Usuario = 1, Total = 50.0m, IsDeleted = false };
            string expectedErrorMessage = "El Id del carrito debe ser mayor que cero.";

            // Act
            var result = await _carritoRepository.Updateasync(invalidDto); 

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Message);

            // Assert
          
            _mockLogger.Verify(log => log.LogError("Falló la validación al actualizar el carrito"), Times.Once);
        }
    }
}