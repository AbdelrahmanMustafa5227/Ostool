﻿using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Logging;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Cars.DeleteCar;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.CarTests
{
    public class GetCarByBrandTests
    {
        private readonly ICarRepository _carRepository = Substitute.For<ICarRepository>();
        private readonly ITestableLogger<GetByBrandCommandHandler> _logger = Substitute.For<ITestableLogger<GetByBrandCommandHandler>>();
        private readonly GetByBrandCommandHandler _handler;

        public GetCarByBrandTests()
        {
            _handler = new GetByBrandCommandHandler(_carRepository, _logger);
        }

        [Fact]
        public async Task NoCarHasThisBrand_ShouldReturnEmptyList()
        {
            // Arrange
            var command = new GetByBrandCommand("Brand", 1);
            _carRepository.GetAllByBrand(command.Brand, command.pageNumber).Returns(new QueryResult<Car>(new List<Car>()));
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Value.Items, new List<GetByBrandResponse>());
            _logger.Received(0).LogError(Arg.Any<string>());
        }

        [Fact]
        public async Task SomeCarsHasThisBrand_ShouldReturnList()
        {
            // Arrange
            var command = new GetByBrandCommand("Brand", 1);
            var cars = new QueryResult<Car>(new List<Car>());
            _carRepository.GetAllByBrand(command.Brand, 1).Returns(cars);
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            Assert.True(result.IsSuccess);
            _logger.Received(0).LogError(Arg.Any<string>());
        }
    }
}