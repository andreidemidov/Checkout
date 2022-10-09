using Checkout.Application.Baskets.Commands.CreateBasket;
using Checkout.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.Json;

namespace Checkout.IntegrationTests {
    [TestClass]
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>> {
        private readonly HttpClient _httpClient;
        private const string url = "http://localhost:5251/Baskets";
        private const string basketID = "9EBDAAA3-2C6A-4DD2-93FE-063D48086EC5";

        public IntegrationTests() {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task Test_get_basket_item_succeded() {
            var response = await _httpClient.GetAsync($"{url}/{basketID}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            Xunit.Assert.IsAssignableFrom<Domain.Models.Basket>(JsonSerializer.Deserialize<Basket>(result));
        }

        [Fact]
        public async Task Test_create_basket_item() {
            var command = new CreateBasketCommand("customer-3", true);
            var serializedCommand = JsonSerializer.Serialize(command);

            var requestContent = new StringContent(serializedCommand, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, requestContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonSerializer.Deserialize<Guid>(result);
            
            Xunit.Assert.IsType<Guid>(parsedResult);
        }

        [Fact]
        public async Task Test_create_article_item() {
            var command = new Article("test-de-integrare", 10);
            var serializedCommand = JsonSerializer.Serialize(command);

            var requestContent = new StringContent(serializedCommand, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url + $"/{basketID}/article-line", requestContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonSerializer.Deserialize<Guid>(result);

            Xunit.Assert.IsType<Guid>(parsedResult);
        }

        [Fact]
        public async Task Test_update_basket_status() {
            var command = false;
            var serializedCommand = JsonSerializer.Serialize(command);

            var requestContent = new StringContent(serializedCommand, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url + $"/{basketID}", requestContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            Xunit.Assert.Equal("Status updated", result);
        }
    }
}
