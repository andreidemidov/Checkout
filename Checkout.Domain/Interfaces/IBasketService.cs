namespace Checkout.Domain.Interfaces {
    public interface IBasketService {
        Task<bool> BasketExists(Guid Id);
        Task<bool> BasketStatus(Guid Id);
    }
}
