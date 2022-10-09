namespace Checkout.Domain.Models {
    public sealed record Basket(Guid Id,
                                IEnumerable<Article> Articles,
                                int totalNet,
                                double totalGross,
                                string customer,
                                bool paysVAT);
}
