using Checkout.Application.Baskets.Commands.UpdateBasket;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Interfaces;
using Moq;

namespace Checkout.Tests.Baskets.Commands.UpdateBasket {
    public class UpdateBasketHandlerTests {
        private readonly Mock<IBasketRepository> _repo;
        private readonly Mock<IBasketService> _service;
        private Guid basketId;

        public UpdateBasketHandlerTests() {
            basketId = Guid.NewGuid();
            _repo = new Mock<IBasketRepository>();
            _service = new Mock<IBasketService>();
        }

        [Fact]
        public async Task Given_CorrectPayload_When_UpdatingBasket_Then_CorrectMessage_Retuned() {
            //Arrage
            var command = new UpdateBasketCommand(basketId, false);
            _service.Setup(x => x.BasketExists(basketId)).ReturnsAsync(true);
            _repo.Setup(x => x.UpdateBasketStatus(basketId, false)).ReturnsAsync(1);
            
            var handler = new UpdateBasketHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Contains("Status updated", result);
        }

        [Fact]
        public async Task Given_IncorrectBaketId_When_UpdatingBasket_Then_ErroeMessage_Retuned() {
            //Arrage
            var command = new UpdateBasketCommand(basketId, false);
            _service.Setup(x => x.BasketExists(basketId)).ReturnsAsync(false);
            _repo.Setup(x => x.UpdateBasketStatus(basketId, false)).ReturnsAsync(1);

            var handler = new UpdateBasketHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Contains("Basket not found!", result);
        }

        [Fact]
        public async Task Given_To_ServerError_When_UpdatingBasket_Then_ErroeMessage_Retuned() {
            //Arrage
            var command = new UpdateBasketCommand(basketId, true);
            _service.Setup(x => x.BasketExists(basketId)).ReturnsAsync(true);
            _repo.Setup(x => x.UpdateBasketStatus(basketId, false)).ReturnsAsync(0);

            var handler = new UpdateBasketHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Contains("Something went wrong", result);
        }
    }
}
