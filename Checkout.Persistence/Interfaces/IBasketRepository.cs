using Checkout.Domain.Entities;

namespace Checkout.Persistence.Interfaces {
    public interface IBasketRepository {
        Task<int> CreateBasketAsync(Basket basket);
        Task<int> CreateArticleAsync(Guid basketId, Article article);
        Task<int> UpdateBasketStatus(Guid Id, bool status);
        Task<Domain.Models.Basket> GetBasket(Guid Id);
    }
}
