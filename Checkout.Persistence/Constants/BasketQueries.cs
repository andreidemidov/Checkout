namespace Checkout.Persistence.Constants {
    public class BasketQueries {
        public const string QueryInsertBasket = @"
insert into Basket values(newid(), @customer, 1, @vat);
";
        public const string QueryBasketExists = @"
select 1 from Basket where Id = @id;
";
        public const string QueryInsertArticle = @"
insert into Article values(@id, @article, @price, @basketId)";

        public const string QueryUpdateBasketStatus = @"
update Basket set Status = @status where Id = @id;
 ";
        public const string QueryGetBasketStatus = @"
select status from Basket where Id = @id;
";
        public const string QueryGetBasket = @"
select
    Id,
    Customer,
    Vat PaysVAT
from Basket b 
where Id = @id
order by b.Id; 
";
        public const string QueryGetArticles = @"
select
    Article,
    Price
from Article
where BasketId = @id
order by Id";
    }
}
