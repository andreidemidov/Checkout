using Checkout.Domain.Models;
using MediatR;

namespace Checkout.Application.Baskets.Queries.GetBasket {
    public sealed record GetBasketQuery (Guid Id) : IRequest<Basket>;
}
