using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using SWCE.Persistence.Context;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Application.Extension.Validators.CuponValidators.CuponMontoFijoValidators;
using SWCE.Persistence.Repositories;
using SWCE.Domain.Base;
using FluentValidation;

namespace SWCE.Persistence.Test
{
    public class UnitTestCuponMontoFijo
    {
        private E_commerceContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<E_commerceContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new E_commerceContext(options);
        }

        [Fact]
        public async Task GetbyIdasync_ReturnsSuccess_WhenCuponExistsAndIsCorrectType()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var cupon = new CuponMontoFijo { id = 1, Monto = 100, FechaExpiracion = DateTime.Now.AddDays(10) };
            context.Cupones.Add(cupon);
            await context.SaveChangesAsync();

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var createValidatorMock = new Mock<CreateCuponMontoFijoValidator>();
            var updateValidatorMock = new Mock<UpdateCuponMontoFijoValidator>();

            var repository = new CuponMontoFijoRepository(
                context,
                loggerMock.Object,
                createValidatorMock.Object,
                updateValidatorMock.Object
            );

            // Act
            var result = await repository.GetbyIdasync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Cupón obtenido correctamente.", result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<CuponMontoFijo>(result.Data);
        }

        [Fact]
        public async Task GetbyIdasync_ReturnsFailure_WhenCuponDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var createValidatorMock = new Mock<CreateCuponMontoFijoValidator>();
            var updateValidatorMock = new Mock<UpdateCuponMontoFijoValidator>();

            var repository = new CuponMontoFijoRepository(
                context,
                loggerMock.Object,
                createValidatorMock.Object,
                updateValidatorMock.Object
            );

            // Act
            var result = await repository.GetbyIdasync(99);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Cupón no encontrado.", result.Message);
        }

        [Fact]
        public async Task GetbyIdasync_ReturnsFailure_WhenCuponIsNotMontoFijo()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var cupon = new CuponPorcentaje
            {
                id = 2,
                Porcentaje = 10,
                FechaExpiracion = DateTime.Now.AddDays(5)
            };
            context.Cupones.Add(cupon);
            await context.SaveChangesAsync();

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var createValidatorMock = new Mock<CreateCuponMontoFijoValidator>();
            var updateValidatorMock = new Mock<UpdateCuponMontoFijoValidator>();

            var repository = new CuponMontoFijoRepository(
                context,
                loggerMock.Object,
                createValidatorMock.Object,
                updateValidatorMock.Object
            );

            // Act
            var result = await repository.GetbyIdasync(2);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Cupón no es de tipo CuponMontoFijo", result.Message);
        }
    }
}
