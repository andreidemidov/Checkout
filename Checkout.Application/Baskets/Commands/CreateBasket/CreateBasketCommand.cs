using MediatR;

namespace Checkout.Application.Baskets.Commands.CreateBasket {
    public record CreateBasketCommand(string customer, bool paysVAT) : IRequest<Guid?> {
    }
}