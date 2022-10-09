using Checkout.Application.Baskets.Commands.UpdateBasket;
using FluentValidation;

namespace Checkout.Application.Baskets.Commands.CloseBasket {
    public class UpdateBasketValidator : AbstractValidator<UpdateBasketCommand> {
        public UpdateBasketValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).Must(x => x == false || x == true);
        }
    }
}
