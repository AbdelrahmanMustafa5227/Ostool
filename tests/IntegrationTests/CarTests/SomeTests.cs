using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Ostool.Api;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Helpers;
using Ostool.IntegrationTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ostool.IntegrationTests.CarTests
{
    public class SomeTests : IAsyncLifetime, IClassFixture<AppFactory>
    {
        private readonly HttpClient _httpClient;

        public SomeTests(AppFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact(Skip = "waiting for Docker Integration")]
        public async Task NoCarsExistsWithThisBrandName_ReturnsEmptyList()
        {
            // Arrange
            var brandName = "This Brand Doesn't Exist";
            // Act
            var response = await _httpClient.GetAsync($"cars/GetByBrand?brandName={brandName}&page=1");
            var deserializedContent = await response.Content.ReadFromJsonAsync<Paginated<GetCarByBrandResponse>>();
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal([], deserializedContent!.Items);
            Assert.False(response.Headers.TryGetValues("SOmeHeader", out var r));
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }
    }
}