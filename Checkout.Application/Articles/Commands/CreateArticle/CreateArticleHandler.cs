using Checkout.Domain.Entities;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Interfaces;
using MediatR;

namespace Checkout.Application.Articles.Commands.CreateArticle {
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Guid?> {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketService _basketService;

        public CreateArticleHandler(IBasketRepository basketRepository, IBasketService basketService) =>
            (_basketRepository, _basketService) = (basketRepository, basketService);

        public async Task<Guid?> Handle(CreateArticleCommand request, CancellationToken cancellationToken) {
            try {
                var basket = new Basket(request.Id);
                var basketExists = basket.CheckIfBasketExists(_basketService);
                var basketStatus = basket.CheckBasketStatus(_basketService);

                await Task.WhenAll(basketExists, basketStatus);

                if (!string.IsNullOrEmpty(basketExists.Result.ErrorMessage) ||
                    !string.IsNullOrEmpty(basketStatus.Result.ErrorMessage))
                    return Guid.Empty;

                basket.Article = new Article {
                    ArticleName = request.Article.article,
                    Price = request.Article.price
                };

                _ = await _basketRepository.CreateArticleAsync(request.Id,
                                                               basket.Article);

                return basket.Article.Id;
            } 
            catch (Exception e) {
                throw e;
            }
        }
    }
}