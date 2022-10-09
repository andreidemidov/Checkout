using Checkout.Application.Articles.Commands.CreateArticle;
using Checkout.Domain.Entities;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Interfaces;
using Moq;

namespace Checkout.Tests.Articles.Commands.CreateArticle {
    public class CreateArticleHandlerTests {
        private readonly Mock<IBasketRepository> _repo;
        private readonly Mock<IBasketService> _service;
        private Guid basketId;

        public CreateArticleHandlerTests() {
            basketId = Guid.NewGuid();
            _repo = new Mock<IBasketRepository>();
            _service = new Mock<IBasketService>();
        }

        [Fact]
        public async Task Given_CorrectPayload_When_CreatingArticle_Then_BaketId_Retuned() {
            //Arrange
            var article = new Domain.Models.Article("article1", 10);
            var basketId = Guid.NewGuid();
            var command = new CreateArticleCommand(basketId, article);

            var basket = new Basket(basketId);
            _service.Setup(x => x.BasketExists(basketId)).ReturnsAsync(true);
            _service.Setup(x => x.BasketStatus(basketId)).ReturnsAsync(true);

            basket.Article = new Article {
                ArticleName = article.article,
                Price = article.price,
            };

            _repo.Setup(x => x.CreateArticleAsync(basketId, basket.Article)).ReturnsAsync(1);
            
            var handler = new CreateArticleHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Given_IncorrectPayload_When_CreatingArticle_Then_EmptyGuid_Retuned() {
            //Arrange
            var article = new Domain.Models.Article("article1", 10);
            var basketId = Guid.NewGuid();
            var command = new CreateArticleCommand(basketId, article);

            var basket = new Basket(basketId);
            _service.Setup(x => x.BasketExists(basketId)).ReturnsAsync(false);

            var handler = new CreateArticleHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Equal(result, Guid.Empty);
        }

        [Fact]
        public async Task Given_BasketStatus_Closed_When_CreatingBasket_Then_EmptyGuid_Retuned() {
            //Arrange
            var article = new Domain.Models.Article("article1", 10);
            var basketId = Guid.NewGuid();
            var command = new CreateArticleCommand(basketId, article);

            var basket = new Basket(basketId);
            _service.Setup(x => x.BasketStatus(basketId)).ReturnsAsync(false);

            var handler = new CreateArticleHandler(_repo.Object, _service.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Equal(result, Guid.Empty);
        }
    }
}
