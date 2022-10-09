using MediatR;

namespace Checkout.Application.Baskets.Commands.UpdateBasket {
    public record UpdateBasketCommand(Guid Id, bool Status): IRequest<string> { }
}
