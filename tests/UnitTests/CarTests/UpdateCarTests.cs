using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Cars.DeleteCar;
using Ostool.Application.Features.Cars.UpdateCar;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.CarTests
{
    public class UpdateCarTests
    {
        private readonly ICarRepository _carRepository = Substitute.For<ICarRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly IPublisher _publisher = Substitute.For<IPublisher>();
        private readonly ITestableLogger<DeleteCarCommand> _logger = Substitute.For<ITestableLogger<DeleteCarCommand>>();
        private readonly UpdateCarCommandHandler _handler;

        public UpdateCarTests()
        {
            _handler = new UpdateCarCommandHandler(_carRepository, _unitOfWork);
        }

        [Fact]
        public async Task CarNotExist_ShouldFail()
        {
            // Arrange
            var car = new Car { Id = Guid.NewGuid(), Brand = "Nissan", Model = "Sunny", AvgPrice = 120000 };
            var command = new UpdateCarCommand(car.Id, car.Brand, car.Model, car.AvgPrice);
            _carRepository.GetByModelName(command.Model).ReturnsNull();
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
            _logger.Received(0).LogError(Arg.Any<string>());
        }

        [Fact]
        public async Task CarExist_ShouldUpdateInfo()
        {
            // Arrange
            var car = new Car { Id = Guid.NewGuid(), Brand = "Nissan", Model = "Sunny", AvgPrice = 120000 };
            var command = new UpdateCarCommand(car.Id, car.Brand, car.Model, 130000);
            _carRepository.GetByModelName(command.Model).Returns(car);
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            Assert.True(result.IsSuccess);
            await _unitOfWork.Received(1).SaveChangesAsync();
        }
    }
}