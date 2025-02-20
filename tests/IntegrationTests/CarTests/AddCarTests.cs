using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Ostool.Api;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.IntegrationTests.Setup;
using Ostool.IntegrationTests.Setup.Extensions;
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

    public class AddCarTests : IAsyncLifetime, IClassFixture<AppFactory>
    {
        private readonly HttpClient _httpClient;

        private readonly Faker<AddCarCommand> faker = new Faker<AddCarCommand>().WithRecord()
            .RuleFor(x => x.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(x => x.Model, f => f.Vehicle.Model())
            .RuleFor(x => x.AvgPrice, f => f.Random.Decimal(10000, 100000));

        private readonly List<Guid> _ids = new List<Guid>();

        public AddCarTests(AppFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact(Skip = "waiting for Docker Integration")]
        public async Task AddCar_WhenBrandAndModelAreValid_ReturnsCar()
        {
            // Arrange
            var command = faker.Generate();
            // Act
            _httpClient.DefaultRequestHeaders.Add("X-Idempotency", Guid.NewGuid().ToString());
            var response = await _httpClient.PostAsJsonAsync("cars/Add", command);
            var deserializedContent = await response.Content.ReadFromJsonAsync<AddCarResponse>();
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(command.Brand, deserializedContent!.Brand);
            Assert.Equal(command.Model, deserializedContent!.Model);
            Assert.Equal(command.AvgPrice, deserializedContent!.AvgPrice);
            _ids.Add(deserializedContent.Id);
        }

        public async Task DisposeAsync()
        {
            foreach (var id in _ids)
            {
                await _httpClient.DeleteAsync($"cars/Delete?CarId={id}");
            }
        }

        public async Task InitializeAsync() => await Task.CompletedTask;

    }
}