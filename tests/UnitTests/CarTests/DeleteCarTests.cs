using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Cars.DeleteCar;
using Ostool.Domain.Entities;
using Ostool.UnitTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ostool.UnitTests.CarTests
{
    public class DeleteCarTests
    {
        private readonly ICarRepository _carRepository = Substitute.For<ICarRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly IPublisher _publisher = Substitute.For<IPublisher>();
        private readonly ITestableLogger<DeleteCarCommand> _logger = Substitute.For<ITestableLogger<DeleteCarCommand>>();
        private readonly DeleteCarCommandHandler _deleteCarCommandHandler;

        public DeleteCarTests()
        {
            _deleteCarCommandHandler = new DeleteCarCommandHandler(_carRepository, _unitOfWork, _logger, _publisher);
        }

        [Fact]
        public async Task CarNotExist_ShouldFail()
        {
            // Arrange
            var command = new DeleteCarCommand(Guid.NewGuid());
            _carRepository.GetById(command.CarId).ReturnsNull();
            // Act
            var result = await _deleteCarCommandHandler.Handle(command, default);
            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
            _logger.Received(0).LogError(Arg.Any<string>());
        }

        [Fact]
        public async Task CarExist_ShouldSucceed()
        {
            // Arrange
            var car = new Car { Id = Guid.NewGuid() };
            var command = new DeleteCarCommand(car.Id);
            _carRepository.GetById(command.CarId).Returns(car);
            // Act
            var result = await _deleteCarCommandHandler.Handle(command, default);
            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}