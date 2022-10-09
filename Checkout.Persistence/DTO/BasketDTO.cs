using Checkout.Domain.Models;

namespace Checkout.Persistence.Dto {
    public class BasketDTO {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public bool PaysVAT { get; set; }
    }
}
