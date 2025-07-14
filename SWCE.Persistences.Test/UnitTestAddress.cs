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
    public class UnitTestAddress
    {
        public readonly IRepositoryAddress repositoryAddress;

        public UnitTestAddress()
        {
            var options = new DbContextOptionsBuilder<E_commerceContext>()
            .UseInMemoryDatabase(databaseName: "Test_DB")
            .Options;

            var mockLogger = new Mock<ILoggerBase<Address>>();

            var context = new E_commerceContext(options);

            repositoryAddress = new AddressRepository(context, mockLogger.Object);
        }

        [Fact]
        public async void TestAddAddress_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            Address address = null;

            //act
            string message = "Address entity cannot be null.";
            var result = await repositoryAddress.Createasync(address);

            //Arrange
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact] 

        public async void TestUpdateAddress_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            Address address = null;

            //Act
            string message = "Address entity cannot be null";
            var result = await repositoryAddress.Updateasync(address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

    
        [Fact]

        public async void TestDisableAddress_ShouldReturnFailure_WhenUserIsNull()
        {
            //Arrange
            Address address = null;

            //Act
            string message = "Address entity cannot be null";
            var result = await repositoryAddress.DisableAsync(address);

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
            string message = "El id tiene que ser positivo";
            var result = await repositoryAddress.GetbyIdasync(id);

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
            var result = await repositoryAddress.GetbyUserId(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);

        }

        [Fact]

        public async void TestGetbyPredeterminada_ShouldReturnFailure_WhenIdUserIsZero()
        {
            //Arrange
            int id = 0;

            //Atc
            string message = "El id tiene que ser positivo";
            var result = await repositoryAddress.GetbyUserId(id);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserList_WhenUsersNotExist()
        {
            //Arrange
            string message = "Retrieving Address entities";
            var Address = new List<Address>
            {
            };
            

            // Act
            var result = await repositoryAddress.GetAllasync(a => a.IsDeleted == false);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}
