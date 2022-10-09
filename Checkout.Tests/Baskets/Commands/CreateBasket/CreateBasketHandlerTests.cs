using Checkout.Application.Baskets.Commands.CreateBasket;
using Checkout.Domain.Entities;
using Checkout.Persistence.Interfaces;
using Moq;

namespace Checkout.Tests.Baskets.Commands.CreateBasket {
    public class CreateBasketHandlerTests {
        private CreateBasketValidator _validator { get; }
        private readonly Mock<IBasketRepository> _repo;

        public CreateBasketHandlerTests() {
            _validator = new CreateBasketValidator();
            _repo = new Mock<IBasketRepository>();
        }

        public Basket GenerateBasketMock(string customer, bool paysVat) {
            return new Basket {
                Customer = customer,
                PaysVAT = paysVat
            };
        }

            [Fact]
        public async Task Given_CorrectPayload_When_CreatingBasket_Then_BaketId_Retuned() {
            //Arrange
            var basket = GenerateBasketMock("customer", true);
            
            var command = new CreateBasketCommand(basket.Customer, basket.PaysVAT);
            _repo.Setup(x => x.CreateBasketAsync(basket)).ReturnsAsync(1);
            var handler = new CreateBasketHandler(_repo.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Given_IncorrectPayload_When_CreatingBasket_Then_EmptyGuid_Retuned() {
            //Arrange
            var basket = GenerateBasketMock(null, default);

            var command = new CreateBasketCommand(basket.Customer, basket.PaysVAT);
            _repo.Setup(x => x.CreateBasketAsync(basket)).ReturnsAsync(0);
            var handler = new CreateBasketHandler(_repo.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Equal(result, Guid.Empty);
        }

        [Fact]
        public void Given_Null_CustomerProperty_When_CreatingBasket_Then_Receive_Validation_Error() {
            //Arrange
            var command = new CreateBasketCommand(null, false);

            //Act
            var result = _validator.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains("'customer' must not be empty.", result.Errors[0].ToString());
        }
    }
}
