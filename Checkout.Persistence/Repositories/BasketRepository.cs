using Checkout.Domain.Entities;
using Checkout.Domain.Interfaces;
using Checkout.Persistence.Configuration;
using Checkout.Persistence.Constants;
using Checkout.Persistence.Dto;
using Checkout.Persistence.Interfaces;
using Dapper;

namespace Checkout.Persistence.Repositories {
    public class BasketRepository : IBasketRepository, IBasketService {
        private readonly DapperContext _dbContext;

        public BasketRepository(DapperContext dbContext) => _dbContext = dbContext;

        public async Task<int> CreateBasketAsync(Basket basket) {
            using (var connection = _dbContext.CreateConnection()) {
                return await connection.ExecuteAsync(BasketQueries.QueryInsertBasket,
                                                     new {
                                                         customer = basket.Customer,
                                                         vat = basket.PaysVAT
                                                     });
            }
        }

        public async Task<bool> BasketExists(Guid Id) {
            using (var connection = _dbContext.CreateConnection()) {
                return await connection.QueryFirstOrDefaultAsync<bool>(BasketQueries.QueryBasketExists, new { id = Id });
            }
        }

        public async Task<int> CreateArticleAsync(Guid basketId, Article article) {
            using (var connection = _dbContext.CreateConnection()) {
                return await connection.ExecuteAsync(BasketQueries.QueryInsertArticle,
                                                     new {
                                                         id = article.Id,
                                                         article = article.ArticleName,
                                                         price = article.Price,
                                                         basketId = basketId
                                                     });
            }
        }

        public async Task<int> UpdateBasketStatus(Guid Id, bool status) {
            using (var connection = _dbContext.CreateConnection()) {
                return await connection.ExecuteAsync(BasketQueries.QueryUpdateBasketStatus,
                                                     new {
                                                         id = Id,
                                                         status = status
                                                     });
            }
        }

        public async Task<bool> BasketStatus(Guid Id) {
            using (var connection = _dbContext.CreateConnection()) {
                return await connection.QueryFirstOrDefaultAsync<bool>(BasketQueries.QueryGetBasketStatus, new { id = Id });
            }
        }

        public async Task<Domain.Models.Basket> GetBasket(Guid Id) {
            var query = BasketQueries.QueryGetBasket + BasketQueries.QueryGetArticles;
            using (var connection = _dbContext.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id = Id })) {
                var basket = await multi.ReadSingleOrDefaultAsync<BasketDTO>();
                if (basket != null)
                    basket.Articles = (await multi.ReadAsync<Domain.Models.Article>()).ToList();
                
                var totalNet = basket.Articles.Sum(x => Convert.ToInt32(x.price));
                return new Domain.Models.Basket(basket.Id,
                                                basket.Articles,
                                                totalNet,
                                                basket.PaysVAT == true ? (totalNet + (totalNet * 0.1)) : totalNet,
                                                basket.Customer,
                                                basket.PaysVAT);
            }
        }
    }
}
