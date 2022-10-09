namespace Checkout.Domain.Entities {
    public class Article {
        public Guid Id { get; } = Guid.NewGuid();
        public string? ArticleName { get; set; }
        public int Price { get; set; }
    }
}
