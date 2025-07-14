using Microsoft.EntityFrameworkCore;
using Moq;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using SWCE.Persistence.Context;
using SWCE.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Persistence.Test
{
    public class UnitTestItem
    {
        public readonly IRepositoryWishListItem repository;

        public UnitTestItem()
        {
            var options = new DbContextOptionsBuilder<E_commerceContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var mockLogger = new Mock<ILoggerBase<WishListItem>>();

            var context = new E_commerceContext(options);

            repository = new WishListItemRepository(context, mockLogger.Object);
        }

        [Fact]
        public async void TestAddItem_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            WishListItem item = null;

            //act
            string message = "WishListItem entity cannot be null";
            var result = await repository.Createasync(item);

            //Arrange
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestDisableItem_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            WishListItem item = null;

            //Act
            string message = "item entity not found.";
            var result = await repository.DisableAsync(item);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestGetbyId_ShouldReturnFailure_WhenIdIsZero()
        {
            //Arrange
            int id = 0;

            //Atc
            string message = "El id debe ser positivo";
            var result = await repository.GetbyIdasync(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);

        }

        [Fact]

        public async void TestGetbyIdUser_ShouldReturnFailure_WhenIdIsZero()
        {
            //Arrange
            int id = 0;

            //Atc
            string message = "El id tiene que ser positivo";
            var result = await repository.GetbyUserid(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserList_WhenUsersNotExist()
        {
            //Arrange
            string message = "Retrieving item entities";
            var Address = new List<WishListItem>
            {
            };


            // Act
            var result = await repository.GetAllasync(a => a.IsDeleted == false);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}
