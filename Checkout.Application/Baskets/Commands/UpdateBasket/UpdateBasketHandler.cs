using Checkout.Application.Baskets.Commands.UpdateBasket;
using Checkout.Domain.Entities;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Interfaces;
using MediatR;

namespace Checkout.Application.Baskets.Commands.UpdateBasket {
    public class UpdateBasketHandler : IRequestHandler<UpdateBasketCommand, string> {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketService _basketService;

        public UpdateBasketHandler(IBasketRepository basketRepository, IBasketService basketService) =>
            (_basketRepository, _basketService) = (basketRepository, basketService);

        public async Task<string> Handle(UpdateBasketCommand request, CancellationToken cancellationToken) {
            try {
                var basket = new Basket(request.Id);
                var basketExists = await basket.CheckIfBasketExists(_basketService);
                if (!string.IsNullOrEmpty(basketExists.ErrorMessage)) return basketExists.ErrorMessage;

                var result = await _basketRepository.UpdateBasketStatus(request.Id, request.Status);

                return result > 0 ? "Status updated" : "Something went wrong";
            } 
            catch (Exception e) {
                throw e;
            }
        }
    }
}
