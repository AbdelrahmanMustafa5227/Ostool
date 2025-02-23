using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Mappings;
using Ostool.Domain.Entities;
using Ostool.UnitTests.Fixtures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ostool.UnitTests.CarTests
{
    [Collection("Guid")]
    public class AddCarTests : IClassFixture<GuidFixture>
    {
        private readonly ICarRepository _mockCarRepository = Substitute.For<ICarRepository>();
        private readonly IUnitOfWork _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        private readonly ITestableLogger<AddCarCommandHandler> _logger = Substitute.For<ITestableLogger<AddCarCommandHandler>>();
        private readonly IPublisher _publisher = Substitute.For<IPublisher>();
        private readonly AddCarCommandHandler _sut;
        private readonly ITestOutputHelper _outputHelper;
        private readonly GuidFixture _fixture;

        public AddCarTests(ITestOutputHelper outputHelper, GuidFixture fixture)
        {
            _outputHelper = outputHelper;
            _fixture = fixture;
            _sut = new AddCarCommandHandler(_mockCarRepository, _mockUnitOfWork, _logger, _publisher);
        }

        [Fact]
        public async Task ShouldFail_WhenAddingCarThatAlreadyExists()
        {
            // Arrange
            var command = new AddCarCommand("X", "Y", 20000);
            _mockCarRepository.GetByModelName(command.Model).Returns(new Car());
            // Act
            var res = await _sut.Handle(command, default);
            // Assert
            _outputHelper.WriteLine(_fixture.Guid.ToString());
            Assert.True(res.IsFailed);
            Assert.True(res.Error!.StatusCode == System.Net.HttpStatusCode.Conflict);
            _logger.Received(1).LogError(Arg.Is<string>(x => x.StartsWith("A Conflict Error")));
        }

        [Fact]
        public async Task ShouldFail_WhenUnitOfWorkThrows()
        {
            // Arrange
            var command = new AddCarCommand("X", "Y", 20000);
            _mockCarRepository.GetByModelName(command.Model).ReturnsNull();
            _mockUnitOfWork.SaveChangesAsync().Throws(new InvalidOperationException("Some Error Message"));
            // Act
            Func<Task> res = async () => await _sut.Handle(command, default);
            // Assert
            var x = await Assert.ThrowsAsync<InvalidOperationException>(res);
            Assert.Equal("Some Error Message", x.Message);
            _logger.Received(0).LogInformation(Arg.Any<string>());
            _logger.Received(0).LogError(Arg.Any<string>());
        }


        [Fact]
        public void Add_ShouldFail_WhenItAlfdreadyExists()
        {
            int a = 5, b = 10;
            int sum = a + b;
            string str = "Abdo Mustafa";
            int[] list = { 1, 2, 3, 4, 5, 6 };
            int[] emptylist = { };
            DateOnly dateTime = DateOnly.FromDateTime(DateTime.Now);
            var obj1 = new User { Name = "Abdo", Age = 16 };
            var obj2 = new User { Name = "Abdo", Age = 16 };
            var obj3 = new User { Name = "Abdo", Age = 16 };
            List<User> objList = new List<User>() { obj1, obj2 };
            Action action = () => throw new ArgumentNullException("arg");
            // Numbers
            Assert.Equal(15, sum);
            Assert.True(sum > 10);
            Assert.InRange(sum, 0, 50);
            // Strings
            Assert.StartsWith("Abdo", str);
            Assert.EndsWith("Mustafa", str);
            // Collections or Strings
            Assert.NotEmpty(str);
            Assert.NotEmpty(list);
            Assert.Empty(emptylist);
            // Dates
            Assert.Equal(dateTime, DateOnly.FromDateTime(DateTime.Now));
            //Assert.InRange(dateTime, new DateOnly(2025, 1, 29), new DateOnly(2025, 2, 15));
            Assert.True(dateTime > new DateOnly(2025, 1, 29));
            Assert.True(dateTime < new DateOnly(2026, 2, 10));
            // Objects
            Assert.Equivalent(obj1, obj2);
            // Collections
            Assert.Contains<User>(obj2, objList);
            Assert.All(objList, x => Assert.True(x.Age > 10));
            // Exceptions
            var x = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("Value cannot be null. (Parameter 'arg')", x.Message);


        }


        [Fact(Skip = "Just Trying", Timeout = 20)]
        public void Add_ShouldFail_WhenIjtAlreadyExists()
        {

        }

        public class User
        {
            public string Name { get; set; } = null!;
            public int Age { get; set; }
        }

        [Theory]
        [MemberData(nameof(Data))]
        [ClassData(typeof(TestData))]
        public void Test(int a, int b, int sum)
        {
            var sumActual = a + b;

            Assert.Equal(sumActual, sum);
        }

        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>()
            {
                new object[] {2,2,4 },
                new object[] {1,2,3 },
                new object[] {2,4,6 },
                new object[] {2,11,13 }
            };
        }

        public class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { -2, 2, 0 };
                yield return new object[] { -2, 4, 2 };
                yield return new object[] { -2, 0, -2 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}