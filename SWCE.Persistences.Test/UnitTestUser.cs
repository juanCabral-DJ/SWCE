using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using Microsoft.Extensions.Configuration;
using SWCE.Persistence.Repositories;
using SWCE.Infraestructure.Logging;
using Moq;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Domain.Base;
using Microsoft.Extensions.Logging;


namespace SWCE.Persistence.Test
{
    public class UnitTestUser
    {
        public readonly IRepositoryUser repositoryUser;

        public UnitTestUser()
        {
            var mockLogger = new Mock<ILoggerBase<User>>();
            var mockConfiguration = new Mock<IConfiguration>();
            this.repositoryUser = new UserRepository(mockLogger.Object,
            mockConfiguration.Object);
        }

        [Fact]
        public async void TestAddUsers_ShouldReturnFailure_WhenUserIsNull()
        {
            // Arrange
            User user = null;

            // Act
            string message = "User cannot be null";
            var result = await repositoryUser.Createasync(user);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestUpdateUsers_ShouldReturnFailure_WhenUserIsNull()
        {
            // Arrange
            User user = null;

            // Act
            string message = "User cannot be null";
            var result = await repositoryUser.Updateasync(user);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestDisableUsers_ShouldReturnFailure_WhenUserIsNull()
        {
            // Arrange
            User user = null;

            // Act
            string message = "User cannot be null";
            var result = await repositoryUser.Disableasync(user);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestGetByEmail_ShouldReturnFailure_WhenEmailIsNull()
        {
            // Arrange
            string email = null;

            // Act
            string message = "Email cannot be null or empty";
            var result = await repositoryUser.GetByEmail(email);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]

        public async void TestGetbyId_ShouldReturnFailure_WhenIdIsZero()
        {
            //Areange
            int id = 0;

            // Act
            string message = "Id cannot be zero or negative";
            var result = await repositoryUser.GetbyIdasync(id);

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserList_WhenUsersNotExist()
        {
            //Arrange
            string message = "An error occurred while retrieving all Users";
            var User = new List<User>
        {
        };

            // Act
            var result = await repositoryUser.GetAllasync();

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
    }
}
