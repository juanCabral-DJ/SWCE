using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using SWCE.Application.Extension.Validators.CuponValidators.CuponPorcentajeValidators;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SWCE.Persistence.Test
{
    public class CuponPorcentajeTests
    {
        private readonly DbContextOptions<E_commerceContext> _dbOptions;

        public CuponPorcentajeTests()
        {
            _dbOptions = new DbContextOptionsBuilder<E_commerceContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public async Task GetbyIdasync_ReturnsSuccess_WhenCuponExists()
        {
            using var context = new E_commerceContext(_dbOptions);
            var cupon = new CuponPorcentaje { id = 1, Porcentaje = 10 };
            context.Cupones.Add(cupon);
            context.SaveChanges();

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var validatorMock = new Mock<CreateCuponPorcentajeValidator>();
            var updateValidatorMock = new Mock<UpdateCuponPorcentajeValidator>();

            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, validatorMock.Object, updateValidatorMock.Object);

            var result = await repo.GetbyIdasync(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetbyIdasync_ReturnsFailure_WhenCuponNotExists()
        {
            using var context = new E_commerceContext(_dbOptions);

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var validatorMock = new Mock<CreateCuponPorcentajeValidator>();
            var updateValidatorMock = new Mock<UpdateCuponPorcentajeValidator>();

            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, validatorMock.Object, updateValidatorMock.Object);

            var result = await repo.GetbyIdasync(999);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Createasync_ReturnsSuccess_WhenValidationPasses()
        {
            using var context = new E_commerceContext(_dbOptions);

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var createValidator = new CreateCuponPorcentajeValidator();
            var updateValidator = new Mock<UpdateCuponPorcentajeValidator>();

            var cupon = new CuponPorcentaje
            {
                id = 1,
                Porcentaje = 20,
                FechaExpiracion = DateTime.Today.AddDays(1)
            };

            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, createValidator, updateValidator.Object);

            var result = await repo.Createasync(cupon);

            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task Createasync_ReturnsFailure_WhenValidationFails()
        {
            using var context = new E_commerceContext(_dbOptions);

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var createValidator = new CreateCuponPorcentajeValidator();
            var updateValidatorMock = new Mock<UpdateCuponPorcentajeValidator>();

            var cupon = new CuponPorcentaje { id = 1, Porcentaje = 0 };
            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, createValidator, updateValidatorMock.Object);
            var result = await repo.Createasync(cupon);

            Assert.False(result.IsSuccess);
            Assert.Contains("Validación fallida", result.Message);
        }


        [Fact]
        public async Task DisableAsync_ReturnsSuccess_WhenCuponExists()
        {
            using var context = new E_commerceContext(_dbOptions);
            var cupon = new CuponPorcentaje { id = 10, Porcentaje = 15 };
            context.Cupones.Add(cupon);
            context.SaveChanges();

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var validatorMock = new Mock<CreateCuponPorcentajeValidator>();
            var updateValidatorMock = new Mock<UpdateCuponPorcentajeValidator>();

            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, validatorMock.Object, updateValidatorMock.Object);

            var result = await repo.DisableAsync(10);

            Assert.True(result.IsSuccess);
            Assert.True(((CuponPorcentaje)result.Data!).IsDeleted);
        }

        [Fact]
        public async Task DisableAsync_ReturnsFailure_WhenCuponDoesNotExist()
        {
            using var context = new E_commerceContext(_dbOptions);

            var loggerMock = new Mock<ILoggerBase<Cupon>>();
            var validatorMock = new Mock<CreateCuponPorcentajeValidator>();
            var updateValidatorMock = new Mock<UpdateCuponPorcentajeValidator>();

            var repo = new CuponPorcentajeRepository(context, loggerMock.Object, validatorMock.Object, updateValidatorMock.Object);

            var result = await repo.DisableAsync(999);

            Assert.False(result.IsSuccess);
            Assert.Contains("no encontrado", result.Message.ToLower());
        }
    }
}


