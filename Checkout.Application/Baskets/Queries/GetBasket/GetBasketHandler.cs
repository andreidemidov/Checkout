using Checkout.Domain.Models;
using Checkout.Persistence.Interfaces;
using MediatR;

namespace Checkout.Application.Baskets.Queries.GetBasket {
    public class GetBasketHandler : IRequestHandler<GetBasketQuery, Basket> {
        private readonly IBasketRepository _basketRepository;

        public GetBasketHandler(IBasketRepository basketRepository) => _basketRepository = basketRepository;

        public async Task<Basket> Handle(GetBasketQuery request, CancellationToken cancellationToken) {
            return await _basketRepository.GetBasket(request.Id);
        }
    }
}
