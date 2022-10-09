using Checkout.Application.Articles.Commands.CreateArticle;
using Checkout.Application.Baskets.Commands.CreateBasket;
using Checkout.Application.Baskets.Commands.UpdateBasket;
using Checkout.Application.Baskets.Queries.GetBasket;
using Checkout.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Api.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketCommand command) {
            return Ok(await this._mediator.Send(command));
        }

        [HttpPost("{id}/article-line")]
        public async Task<IActionResult> CreateArticle(Guid id, Article article) {
            var command = new CreateArticleCommand(id, article);

            return Ok(await this._mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasketStatus(Guid id, bool status) {
            var command = new UpdateBasketCommand(id, status);

            return Ok(await this._mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(Guid id) {
            var query = new GetBasketQuery(id);
        
            return Ok(await this._mediator.Send(query));
        }

        [HttpGet()]
        public ActionResult<string> Get() {
            return Ok("Test");
        }
    }
}
