using Checkout.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Domain.Entities {
    public class Basket {
        public Guid Id { get; }
        public string Customer { get; set; }
        public bool Status { get; set; }
        public bool PaysVAT { get; set; }
        public Article Article { get; set; }

        public Basket() {
            Id = Guid.NewGuid();
        }

        public Basket(Guid id) => this.Id = id;

        public async Task<ValidationResult> CheckIfBasketExists(IBasketService basketService) {
            var result = new ValidationResult(string.Empty);
            var basketExists = await basketService.BasketExists(Id);
            
            if (!basketExists)
                result.ErrorMessage = "Basket not found!";

            return result;
        }

        public async Task<ValidationResult> CheckBasketStatus(IBasketService basketService) {
            var result = new ValidationResult(string.Empty);
            var basketStatus = await basketService.BasketStatus(Id);

            if (!basketStatus)
                result.ErrorMessage = "Basket closed!";

            return result;
        }
    }
}
