using Checkout.Domain.Models;
using FluentValidation;

namespace Checkout.Application.Articles.Commands.CreateArticle {
    public class CreateArticleValidator : AbstractValidator<CreateArticleCommand> {
        public CreateArticleValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Article.article).NotEmpty();
            RuleFor(x => x.Article.price).GreaterThanOrEqualTo(0);
        }
    }
}
