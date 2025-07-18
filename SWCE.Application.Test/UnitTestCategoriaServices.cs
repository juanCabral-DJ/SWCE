/*using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using Xunit;

namespace SWCE.Application.Test
{
    public class UnitTestCategoriaServices
    {
        private readonly ICategoriaServices _categoriaServices;
        private readonly Mock<IRepositorioCategoria> _categoriaRepoMock;
        private readonly Mock<ILoggerBase<CategoriaServices>> _loggerMock;

        public UnitTestCategoriaServices()
        {
            _categoriaRepoMock = new Mock<IRepositorioCategoria>();
            _loggerMock = new Mock<ILoggerBase<CategoriaServices>>();
            var configMock = new Mock<IConfiguration>();

            _categoriaServices = new CategoriaServices(
                _categoriaRepoMock.Object,
                _loggerMock.Object,
                configMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess_WhenRepositorySucceeds()
        {
            // Arrange
            var dto = new CreateCategoriaDto { Nombre = "Tecnología" };
            _categoriaRepoMock.Setup(r => r.Createasync(It.IsAny<Categoria>()))
                .ReturnsAsync(OperationResult.Success("Creado correctamente"));

            // Act
            var result = await _categoriaServices.Createasync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Creado correctamente", result.Message);
        }

        [Fact]
        public async Task GetById_ReturnsSuccess_WhenCategoriaExists()
        {
            // Arrange
            _categoriaRepoMock.Setup(r => r.GetbyIdasync(1))
                .ReturnsAsync(OperationResult.Success(new Categoria { id = 1, Nombre = "Libros" }));

            // Act
            var result = await _categoriaServices.GetbyId(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.IsType<OperationResult>(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsSuccess_WhenUpdateSucceeds()
        {
            // Arrange
            var dto = new UpdateCategoriaDto { Id = 1, Nombre = "Electrónica" };
            _categoriaRepoMock.Setup(r => r.Updateasync(It.IsAny<Categoria>()))
                .ReturnsAsync(OperationResult.Success("Actualizado"));

            // Act
            var result = await _categoriaServices.Updateasync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Actualizado", result.Message);
        }

        [Fact]
        public async Task DisableAsync_ReturnsSuccess_WhenDisableSucceeds()
        {
            // Arrange
            _categoriaRepoMock.Setup(r => r.DisableAsync(1))
                .ReturnsAsync(OperationResult.Success("Deshabilitado"));

            // Act
            var result = await _categoriaServices.Disableasync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Deshabilitado", result.Message);
        }

        [Fact]
        public async Task ObtenerActivasAsync_ReturnsCategorias_WhenSuccess()
        {
            // Arrange
            var lista = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Ropa", Estado = true }
            };
            _categoriaRepoMock.Setup(r => r.ObtenerActivasAsync())
                .ReturnsAsync(lista);

            // Act
            var result = await _categoriaServices.ObtenerActivasAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Ropa", result.First().Nombre);
        }
    }
}*/

using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SWCE.Application.Test
{
    public class UnitTestCategoriaServices
    {
        public readonly ICategoriaServices categoriaServices;
        private readonly Mock<IRepositorioCategoria> mockCategoriaRepo;
        private readonly Mock<ILoggerBase<CategoriaServices>> mockLogger;
        private readonly Mock<IConfiguration> mockConfiguration;

        public UnitTestCategoriaServices()
        {
            mockCategoriaRepo = new Mock<IRepositorioCategoria>();
            mockLogger = new Mock<ILoggerBase<CategoriaServices>>();
            mockConfiguration = new Mock<IConfiguration>();

            // Simulando por asi decirlo las respuestas para los métodos del repositorio
            mockCategoriaRepo.Setup(x => x.Createasync(It.IsAny<Categoria>()))
                .ReturnsAsync(OperationResult.Success("Categoría creada exitosamente"));

            mockCategoriaRepo.Setup(x => x.Updateasync(It.IsAny<Categoria>()))
                .ReturnsAsync(OperationResult.Success("Categoría actualizada exitosamente"));

            mockCategoriaRepo.Setup(x => x.GetbyIdasync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult.Success("Categoría encontrada", new Categoria()));

            mockCategoriaRepo.Setup(x => x.GetAllasync(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .ReturnsAsync(OperationResult.Success("Categorías encontradas", new List<Categoria>()));

            mockCategoriaRepo.Setup(x => x.ObtenerActivasAsync())
                .ReturnsAsync(new List<Categoria>());

            mockCategoriaRepo.Setup(x => x.DisableAsync(It.IsAny<int>()))
                .ReturnsAsync(OperationResult.Success("Categoría deshabilitada exitosamente"));

            categoriaServices = new CategoriaServices(
                mockCategoriaRepo.Object,
                mockLogger.Object,
                mockConfiguration.Object);
        }

        [Fact]
        public async void Createasync_ShouldSetDefaultAuditProperties()
        {
            // Arrange
            var categoriaDto = new CreateCategoriaDto
            {
                Nombre = "Ropa",
                Descripcion = "Prendas de vestir"
            };

            Categoria capturedEntity = null!;
            mockCategoriaRepo.Setup(x => x.Createasync(It.IsAny<Categoria>()))
                .Callback<Categoria>(entity => capturedEntity = entity)
                .ReturnsAsync(OperationResult.Success("Categoría creada"));

            // Act
            await categoriaServices.Createasync(categoriaDto);

            // Assert
            Assert.NotNull(capturedEntity);
            Assert.False(capturedEntity.IsDeleted); // Verifica que IsDeleted es false por defecto
        }

        [Fact]
        public async void Createasync_ShouldReturnSuccess_WithValidData()
        {
            // Arrange
            var categoriaDto = new CreateCategoriaDto
            {
                Nombre = "Categoría válida",
                Descripcion = "Descripción válida"
            };

            // Act
            var result = await categoriaServices.Createasync(categoriaDto);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            mockCategoriaRepo.Verify(x => x.Createasync(It.IsAny<Categoria>()), Times.Once);
        }

        [Fact]
        public async void Updateasync_ShouldReturnFailure_WhenIdIsZero()
        {
            // Arrange
            var categoriaDto = new UpdateCategoriaDto
            {
                Id = 0,
                Nombre = "Nombre válido",
                Descripcion = "Descripción válida"
            };

            // Act
            var result = await categoriaServices.Updateasync(categoriaDto);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void Updateasync_ShouldReturnSuccess_WithValidData()
        {
            // Arrange
            var categoriaDto = new UpdateCategoriaDto
            {
                Id = 1,
                Nombre = "Nombre válido",
                Descripcion = "Descripción válida"
            };

            // Act
            var result = await categoriaServices.Updateasync(categoriaDto);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            mockCategoriaRepo.Verify(x => x.Updateasync(It.IsAny<Categoria>()), Times.Once);
        }

        [Fact]
        public async void GetbyId_ShouldReturnFailure_WhenIdIsZero()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await categoriaServices.GetbyId(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void GetbyId_ShouldReturnSuccess_WithValidId()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await categoriaServices.GetbyId(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            mockCategoriaRepo.Verify(x => x.GetbyIdasync(id), Times.Once);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnSuccess_WithEmptyList()
        {
            // Arrange
            Expression<Func<Categoria, bool>> filter = c => c.IsDeleted == false;

            // Act
            var result = await categoriaServices.GetAllAsync(filter);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Empty((List<Categoria>)result.Data!);
            mockCategoriaRepo.Verify(x => x.GetAllasync(filter), Times.Once);
        }

        [Fact]
        public async void ObtenerActivasAsync_ShouldReturnEmptyList_WhenNoActiveCategories()
        {
            // Act
            var result = await categoriaServices.ObtenerActivasAsync();

            // Assert
            Assert.IsType<List<Categoria>>(result);
            Assert.Empty(result);
            mockCategoriaRepo.Verify(x => x.ObtenerActivasAsync(), Times.Once);
        }

        [Fact]
        public async void Disableasync_ShouldReturnFailure_WhenIdIsZero()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await categoriaServices.Disableasync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Contains("El ID debe ser positivo", result.Message);
        }

        [Fact]
        public async void Disableasync_ShouldReturnSuccess_WithValidId()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await categoriaServices.Disableasync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            mockCategoriaRepo.Verify(x => x.DisableAsync(id), Times.Once);
        }
    }
}
