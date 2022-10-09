using FluentValidation;

namespace Checkout.Application.Baskets.Commands.CreateBasket {
    public class CreateBasketValidator : AbstractValidator<CreateBasketCommand> {
        public CreateBasketValidator() {
            RuleFor(x => x.customer).NotEmpty();
            RuleFor(x => x.paysVAT).Must(x => x == false || x == true);
        }
    }
}
