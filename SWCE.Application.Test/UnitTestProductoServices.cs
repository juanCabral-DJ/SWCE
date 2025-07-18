using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities;
using SWCE.Domain.Repository;
using SWCE.Infraestructure.Logging;
using Xunit;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWCE.Application.Test
{
    public class UnitTestProductoServices
    {
        private readonly IProductoServices _productoServices;
        private readonly Mock<IRepositorioProducto> _productoRepositoryMock;

        public UnitTestProductoServices()
        {
            _productoRepositoryMock = new Mock<IRepositorioProducto>();
            var loggerMock = new Mock<ILoggerBase<ProductoService>>();
            var configMock = new Mock<IConfiguration>();

            _productoServices = new ProductoService(
                _productoRepositoryMock.Object,
                loggerMock.Object,
                configMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess_WhenValidProducto()
        {
            var dto = new CreateProductoDto { Nombre = "Libro", Precio = 10 };
            _productoRepositoryMock.Setup(x => x.Createasync(It.IsAny<Producto>()))
                .ReturnsAsync(OperationResult.Success("Producto creado"));

            var result = await _productoServices.Createasync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Producto creado", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsSuccess_WhenValidUpdate()
        {
            var dto = new UpdateProductoDto { Id = 1, Nombre = "Libro Nuevo" };
            _productoRepositoryMock.Setup(x => x.Updateasync(It.IsAny<Producto>()))
                .ReturnsAsync(OperationResult.Success("Producto actualizado"));

            var result = await _productoServices.Updateasync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Producto actualizado", result.Message);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSuccess_WithData()
        {
            _productoRepositoryMock.Setup(x => x.GetAllasync(It.IsAny<Expression<System.Func<Producto, bool>>>()))
                .ReturnsAsync(OperationResult.Success("Productos recuperados", new List<Producto>()));

            var result = await _productoServices.GetAllAsync(p => true);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetById_ReturnsSuccess_WhenFound()
        {
            _productoRepositoryMock.Setup(x => x.GetbyIdasync(1))
                .ReturnsAsync(OperationResult.Success("Producto recuperado", new Producto { id = 1, Nombre = "Libro" }));

            var result = await _productoServices.GetbyId(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task DisableAsync_ReturnsFailure_WhenIdIsInvalid()
        {
            var result = await _productoServices.Disableasync(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del producto debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task DisableAsync_ReturnsSuccess_WhenDisabled()
        {
            _productoRepositoryMock.Setup(x => x.DisableAsync(1))
                .ReturnsAsync(OperationResult.Success("Producto deshabilitado"));

            var result = await _productoServices.Disableasync(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Producto deshabilitado", result.Message);
        }
    }
}