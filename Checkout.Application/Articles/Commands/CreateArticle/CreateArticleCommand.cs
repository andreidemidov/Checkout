using Checkout.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Application.Articles.Commands.CreateArticle {
    public record CreateArticleCommand(Guid Id,
                                       Article Article) : IRequest<Guid?> {
    }
}