using Checkout.Domain.Entities;
using Checkout.Persistence.Interfaces;
using MediatR;

namespace Checkout.Application.Baskets.Commands.CreateBasket {
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, Guid?> {
        private readonly IBasketRepository _basketRepository;

        public CreateBasketHandler(IBasketRepository basketRepository) => _basketRepository = basketRepository;

        public async Task<Guid?> Handle(CreateBasketCommand request, CancellationToken cancellationToken) {
            try {
                var basket = new Basket {
                    Customer = request.customer,
                    PaysVAT = request.paysVAT
                };

                var result = await _basketRepository.CreateBasketAsync(basket);

                return result > 0 ? basket.Id : Guid.Empty;
            } 
            catch (Exception ex) {
                throw ex;
            }

        }
    }
}