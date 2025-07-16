using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_User;
using SWCE.Aplicatition.Extension.Validators_Registro.UserValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
using SWCE.Domain.Base;
using SWCE.Domain.Entities.Configuration.User_Perfil;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Test
{
    public class UnitTestUserServices
    {
        public readonly IUserServices userServices;
        public readonly IRepositoryUser repositoryUser;

        public UnitTestUserServices()
        {
            var mockUserRepository = new Mock<IRepositoryUser>();
            var CreateUserValidator = new  CreateUserValidator(mockUserRepository.Object);
            var UpdateUserValidator = new  UpdateUserValidator(mockUserRepository.Object);
            var mockLogger = new Mock<ILoggerBase<UserServices>>();
            var mockConfiguration = new Mock<IConfiguration>();

            mockUserRepository.Setup(x => x.GetByEmail("string@gmail.com"))
    .ReturnsAsync(new OperationResult
    {
        IsSuccess = true,
        Message = "Usuario encontrado"
    });

            userServices = new UserServices(
               mockUserRepository.Object,
               mockLogger.Object,
               mockConfiguration.Object,
                CreateUserValidator,
                UpdateUserValidator);
             
        }

        [Fact]
        public async void UserValidatorAdd_WhenNameIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "",
                apellido = "Cabral",
                id_rol = 1,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El nombre es obligatorio.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenNameExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "adddddddddddddddddddddddddd",
                apellido = "Cabral",
                id_rol = 1,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El nombre no puede tener más de 20 caracteres.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenLastNameIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "",
                id_rol = 1,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El apellido es obligatorio.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
        
        [Fact]
        public async void UserValidatorAdd_WhenLastNameExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "El apellido no puede tener más de 20 caracteres.",
                id_rol = 1,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El apellido no puede tener más de 20 caracteres.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenRoleIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 0,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El rol es obligatorio. El rol debe ser un número positivo.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenRoleIdIsNegative_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = -1,
                email = "Email@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = " El rol debe ser un número positivo.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 1,
                email = "",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El email es obligatorio. El formato del email no es válido.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailHasInvalidFormat_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 1,
                email = "email.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = " El formato del email no es válido.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailIsNotUnique_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 1,
                email = "string@gmail.com",
                password = "123456789",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "El email ya está en uso. Por favor, utiliza otro email.";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void UserValidatorAdd_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 1,
                email = "string1@gmail.com",
                password = " ",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = "La contraseña es obligatoria. Debe tener más de 8 caracteres";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void LoginValidatorAdd_WhenPasswordIsTooShort_ShouldHaveValidationError()
        {
            //Arrange
            var User = new CreateUserDto
            {
                Nombre = "Juan",
                apellido = "Cabral",
                id_rol = 1,
                email = "string1@gmail.com",
                password = "12345",
                Fecha_Creacion = DateTime.UtcNow

            };

            //Act
            string message = " Debe tener más de 8 caracteres";
            var result = await userServices.Createasync(User);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

    }
}
