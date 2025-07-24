// SWCE.Tests/Repositories/ItemCarritoRepositoryTests.cs

using Microsoft.EntityFrameworkCore;
using Moq;
using SWCE.Application.Extension.Validators.ItemCarritoValidator;
using SWCE.Domain.Entities;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using Xunit;

namespace SWCE.Persistence.Test
{
    public class UnitTestItemCarrito
    {
        private readonly DbContextOptions<E_commerceContext> _dbOptions;
        private readonly Mock<ILoggerBase<ItemCarrito>> _mockLogger;
        private readonly CreateItemCarritoValidator _validator;

        public UnitTestItemCarrito()
        {
            _dbOptions = new DbContextOptionsBuilder<E_commerceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockLogger = new Mock<ILoggerBase<ItemCarrito>>();
            _validator = new CreateItemCarritoValidator();
        }

        private E_commerceContext CreateContext() => new E_commerceContext(_dbOptions);


        [Fact]
        public async Task AddItemAsync_ShouldAddNewItem_WhenItemDoesNotExist()
        {
            // Arrange
            await using var context = CreateContext();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);
            var newItem = new ItemCarrito { CarritoId = 1, IdProducto = 1, Cantidad = 2, PrecioUnitario = 10, SubTotal = 20 };

            // Act
            var result = await repository.AddItemAsync(newItem);
            var itemInDb = await context.ItemsCarrito.FirstOrDefaultAsync(i => i.IdProducto == 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(itemInDb);
            Assert.Equal(2, itemInDb.Cantidad);
        }

        [Fact]
        public async Task AddItemAsync_ShouldUpdateQuantityAndSubtotal_WhenItemExists()
        {
            // Arrange
            await using var context = CreateContext();
            var existingItem = new ItemCarrito { Id = 1, CarritoId = 1, IdProducto = 1, Cantidad = 1, PrecioUnitario = 15, SubTotal = 15 };
            context.ItemsCarrito.Add(existingItem);
            await context.SaveChangesAsync();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);
            var itemToAdd = new ItemCarrito { CarritoId = 1, IdProducto = 1, Cantidad = 2, PrecioUnitario = 15.50m };

            // Act
            var result = await repository.AddItemAsync(itemToAdd);
            var itemInDb = await context.ItemsCarrito.FindAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(itemInDb);
            Assert.Equal(3, itemInDb.Cantidad);
            Assert.Equal(15.50m, itemInDb.PrecioUnitario);
            Assert.Equal(3 * 15.50m, itemInDb.SubTotal);
        }

        [Fact]
        public async Task GetItemsByCarritoIdAsync_ShouldReturnOnlyItemsForGivenCarritoId()
        {
            // Arrange
            await using var context = CreateContext();
            context.ItemsCarrito.AddRange(
                new ItemCarrito { CarritoId = 1, IdProducto = 101 },
                new ItemCarrito { CarritoId = 1, IdProducto = 102 },
                new ItemCarrito { CarritoId = 2, IdProducto = 103 }
            );
            await context.SaveChangesAsync();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);

            // Act
            var result = await repository.GetItemsByCarritoIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.True(result.All(item => item.CarritoId == 1));
        }

        [Fact]
        public async Task RemoveItemAsync_ShouldSoftDeleteItem_WhenItemExists()
        {
            // Arrange
            await using var context = CreateContext();
            var itemToDelete = new ItemCarrito { Id = 5, CarritoId = 1, IdProducto = 1, Cantidad = 1 };
            context.ItemsCarrito.Add(itemToDelete);
            await context.SaveChangesAsync();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);

            // Act
            var result = await repository.RemoveItemAsync(5);
            var itemInDb = await context.ItemsCarrito.IgnoreQueryFilters().FirstOrDefaultAsync(i => i.Id == 5);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(itemInDb);
            Assert.True(itemInDb.IsDeleted);
        }

        // --- NUEVAS PRUEBAS ---

        [Fact]
        public async Task AddItemAsync_ShouldReturnFailure_WhenValidationFails()
        {
            // Arrange
            await using var context = CreateContext();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);
            var invalidItem = new ItemCarrito { CarritoId = 1, IdProducto = 1, Cantidad = 0, PrecioUnitario = 10 };

            // Act
            var result = await repository.AddItemAsync(invalidItem);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("La cantidad debe ser mayor a 0.", result.Message);
        }

        [Fact]
        public async Task RemoveItemAsync_ShouldReturnFailure_WhenItemDoesNotExist()
        {
            // Arrange
            await using var context = CreateContext();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);

            // Act
            var result = await repository.RemoveItemAsync(999); 

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("ItemCarrito con ID 999 no encontrado.", result.Message);
        }

        [Fact]
        public async Task GetItemsByCarritoIdAsync_ShouldReturnEmptyList_WhenNoItemsFound()
        {
            // Arrange
            await using var context = CreateContext();
            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);

            // Act
            var result = await repository.GetItemsByCarritoIdAsync(123); 

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ClearItemsByCarritoIdAsync_ShouldRemoveAllItems_WhenItemsExist()
        {
            // Arrange
            await using var context = CreateContext();
            int carritoId = 7;
            context.ItemsCarrito.AddRange(
                new ItemCarrito { CarritoId = carritoId, IdProducto = 1 },
                new ItemCarrito { CarritoId = carritoId, IdProducto = 2 }
            );
            await context.SaveChangesAsync();

            var repository = new ItemCarritoRepository(context, _mockLogger.Object, _validator);

            // Act
            var result = await repository.ClearItemsByCarritoIdAsync(carritoId);
            var itemsCount = await context.ItemsCarrito.CountAsync(i => i.CarritoId == carritoId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(0, itemsCount);
        }
    }
}