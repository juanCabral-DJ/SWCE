using Microsoft.Extensions.Configuration;
using Moq;
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Domain.Base;
using SWCE.Aplicatition.Extension.Validators_Registro.WishListItemValidator;
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
    public class UnitTestItemServices
    {
        public readonly IWishListItemServices ItemServices;

        public UnitTestItemServices()
        {
            var mockItemRepository = new Mock<IRepositoryWishListItem>();
            var validator = new CreateWishListItemValidator();
            var mockLogger = new Mock<ILoggerBase<WishListItemServices>>();
            var mockConfiguration = new Mock<IConfiguration>();



            ItemServices = new WishListItemServices(
               mockItemRepository.Object,
               mockLogger.Object,
               mockConfiguration.Object,
               validator);

        }

        [Fact]
        public async void WishListItemValidator_WhenUserIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var item = new CreateItemDto
            {
                id_producto = 1,
                Id_Usuario = 0
            };

            //Act
            string message = "El id del usuario debe ser mayor que 0";
            var result = await ItemServices.Createasync(item);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async void WishListItemValidator_WhenProductIdIsZero_ShouldHaveValidationError()
        {
            //Arrange
            var item = new CreateItemDto
            {
                id_producto = 0,
                Id_Usuario = 1
            };

            //Act
            string message = "El id del producto debe ser mayor que 0";
            var result = await ItemServices.Createasync(item);

            //Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

    }
}
