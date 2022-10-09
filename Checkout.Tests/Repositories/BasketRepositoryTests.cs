using Checkout.Domain.Entities;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Interfaces;
using Moq;

namespace Checkout.Tests.Repositories {
    public class BasketRepositoryTests {
        private readonly Mock<IBasketRepository> repoMock;
        private readonly Mock<IBasketService> serviceMock;

        public BasketRepositoryTests() {
            repoMock = new Mock<IBasketRepository>();
            serviceMock = new Mock<IBasketService>();
        }

        [Fact]
        public void Can_create_basket() {
            //Arrange
            var basket = It.IsAny<Basket>();
            repoMock.Setup(x => x.CreateBasketAsync(basket)).ReturnsAsync(1);
            //Act
            var returnedValue = repoMock.Object.CreateBasketAsync(It.IsAny<Basket>());

            //Assert
            Assert.NotNull(returnedValue);
            Assert.IsAssignableFrom<int>(returnedValue.Result);
        }

        [Fact]
        public void Should_update_basket_when_pass_correct_parameters_value() {
            //Arrange
            var basketGuid = It.IsAny<Guid>();
            repoMock.Setup(x => x.UpdateBasketStatus(basketGuid, false)).ReturnsAsync(1);
            
            //Act
             var result = repoMock.Object.UpdateBasketStatus(basketGuid, false);

            //Assert
            Assert.NotEqual(0, result.Result);
        }

        [Fact]
        public void Should_get_basket_object_when_id_exists() {
            //Arrange
            var basketId = It.IsAny<Guid>();
            repoMock.Setup(x => x.GetBasket(basketId))
                    .Returns(Task.FromResult(new Domain.Models.Basket(default,
                                                                           default,
                                                                           default,
                                                                           default,
                                                                           default,
                                                                           default)));

            //Act
            var result = repoMock.Object.GetBasket(basketId);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Domain.Models.Basket>(result.Result);
        }

        [Fact]
        public void Should_get_true_when_basket_exists_in_db() {
            //Arrange
            var basketId = It.IsAny<Guid>();
            serviceMock.Setup(x => x.BasketExists(basketId)).ReturnsAsync(true);

            //Act
            var result = serviceMock.Object.BasketExists(basketId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Result);
        }

        [Fact]
        public void Should_get_false_when_basket_isnt_exists_in_db() {
            //Arrange
            var basketId = It.IsAny<Guid>();
            serviceMock.Setup(x => x.BasketExists(basketId)).ReturnsAsync(false);

            //Act
            var result = serviceMock.Object.BasketExists(basketId);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Result);
        }

        [Fact]
        public void Should_get_basket_status() {
            var basketId = It.IsAny<Guid>();
            serviceMock.Setup(x => x.BasketStatus(basketId)).ReturnsAsync(true);

            //Act
            var result = serviceMock.Object.BasketStatus(basketId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Result);
        }
    }
}
