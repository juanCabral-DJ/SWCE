
using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplicatition.Extension.Validators_Registro.AddressValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
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
            var mockCreateUserValidator = new Mock<CreateAddressValidator>();
            var mockLogger = new Mock<ILoggerBase<AddressServices>>();
            var mockConfiguration = new Mock<IConfiguration>();



            AddressServices = new AddressServices(
               mockAddressRepository.Object,
               mockCreateUserValidator.Object,
               mockLogger.Object,
               mockConfiguration.Object);

        }

        [Fact]
        public async void AddressValidatorAdd_WhenUserIdIsZero_ShouldHaveValidationError()
        {

        }

        [Fact]

        public async void AddressValidatorAdd_WhenStreetIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidator_WhenStreetExceedsMaxLength_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenCityIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenCityExceedsMaxLength_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenStateIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenStateExceedsMaxLength_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenPostalCodeIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenPostalCodeExceedsMaxLength_ShouldHaveValidationError()
        {
        } 

        [Fact]
        public async void AddressValidatorAdd_WhenCountryIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void AddressValidatorAdd_WhenCountryExceedsMaxLength_ShouldHaveValidationError()
        {

        }


    }
}
