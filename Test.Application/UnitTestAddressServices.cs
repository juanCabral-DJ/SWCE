
using Antlr.Runtime;
using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Extension.Validators_Registro.AddressValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Application.Test
{
    public class UnitTestAddressServices
    {
        public readonly IAddressServices AddressServices;

        public UnitTestAddressServices()
        {
            var mockAddressRepository = new Mock<IRepositoryAddress>();
            var mockCreateUserValidator = new CreateAddressValidator();
            var mockLogger = new Mock<ILoggerBase<AddressServices>>();
            var mockConfiguration = new Mock<IConfiguration>();



            AddressServices = new AddressServices(
               mockAddressRepository.Object,
               mockCreateUserValidator,
               mockLogger.Object,
               mockConfiguration.Object);

        }

        [Fact]
        public async void AddressValidatorAdd_WhenUserIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "0001",
                ciudad = "Cabrera",
                ID_Usuario = 0,
                estado_provincia = "Peravia",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "El id del usuario no puede estar vacio. El id del usuario debe ser mayor que 0";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }
 

        [Fact]

        public async void AddressValidatorAdd_WhenStreetIsEmpty_ShouldHaveValidationError()
        {

            //Arrange
            var Address = new CreateAddressDto
            {
                calle = " ",
                ciudad = "Cabrera",
                ID_Usuario = 1,
                estado_provincia = "Peravia",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "La calle no puede estar vacia.";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidator_WhenStreetExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeeeeeeeeeeeeeeeeee",
                ciudad = "Cabrera",
                ID_Usuario = 1,
                estado_provincia = "Peravia",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = " La calle no puede pasar de los 20 caracteres";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenCityIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = " ",
                ID_Usuario = 1,
                estado_provincia = "Peravia",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "La ciudad no puede estar vacia.";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenCityExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "Peravia",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = " La ciudad no puede pasar de los 20 caracteres";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenStateIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "El estado o provincia no puede estar vacio";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenStateExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "ooooooooooooooooooooooooooooooooooooo",
                codigo_postal = "94000",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = " El estado o provincia no puede pasar de los 20 caracteres";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenPostalCodeIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "ooooooooooo",
                codigo_postal = "",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "El codigo postal no puede estar vacio";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenPostalCodeExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "ooooooooooo",
                codigo_postal = "uuuuuuuuuuuuuuuuuuuuuuuuu",
                pais = "RD",
                Es_predeterminada = true

            };

            //Act
            string message = "El codigo postal no puede pasar de los 20 caracteres";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        } 

        [Fact]
        public async void AddressValidatorAdd_WhenCountryIsEmpty_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "ooooooooooo",
                codigo_postal = "uuuuuuuuuuuuuuu",
                pais = " ",
                Es_predeterminada = true

            };

            //Act
            string message = "El pais no puede estar vacio";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void AddressValidatorAdd_WhenCountryExceedsMaxLength_ShouldHaveValidationError()
        {
            //Arrange
            var Address = new CreateAddressDto
            {
                calle = "weeeeeeeee",
                ciudad = "aaaaaaaaa",
                ID_Usuario = 1,
                estado_provincia = "ooooooooooo",
                codigo_postal = "uuuuuuuuuuuuuuu",
                pais = "rrrrrrrrrrrrrrrrrrrrrrrr",
                Es_predeterminada = true

            };

            //Act
            string message = "El pais no puede pasar de los 20 caracteres";
            var result = await AddressServices.Createasync(Address);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }


    }
}
