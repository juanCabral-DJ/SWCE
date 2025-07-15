using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Extension.Mapeo_Registro.Mapeo_User;
using SWCE.Aplicatition.Extension.Validators_Registro.UserValidator;
using SWCE.Aplicatition.Interfaces.Repositories.User_Perfil;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Aplicatition.Services;
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

        public UnitTestUserServices()
        {
            var mockUserRepository = new Mock<IRepositoryUser>();
            var mockCreateUserValidator = new Mock<CreateUserValidator>();
            var mockUpdateUserValidator = new Mock<UpdateUserValidator>();
            var mockLogger = new Mock<ILoggerBase<UserServices>>();
            var mockConfiguration = new Mock<IConfiguration>();



            userServices = new UserServices(
               mockUserRepository.Object,
               mockLogger.Object,
               mockConfiguration.Object,
               mockCreateUserValidator.Object,
               mockUpdateUserValidator.Object);
             
        }

        [Fact]
        public async void UserValidatorAdd_WhenNameIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenNameExceedsMaxLength_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenLastNameIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenLastNameExceedsMaxLength_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenRoleIdIsZero_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenRoleIdIsNegative_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailHasInvalidFormat_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenEmailIsNotUnique_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void UserValidatorAdd_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void LoginValidatorUpdate_WhenEmailIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void LoginValidatorUpdate_WhenEmailHasInvalidFormat_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void LoginValidatorUpdate_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {

        }

        [Fact]
        public async void LoginValidator_UpdateWhenPasswordIsTooShort_ShouldHaveValidationError()
        {

        }
    }
}
