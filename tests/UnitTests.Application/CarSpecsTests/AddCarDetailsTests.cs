using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.CarSpecs.AddCarDetails;
using Ostool.Domain.Entities;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.CarSpecsTests
{
    public class AddCarDetailsTests
    {
        private readonly ICarRepository _carRepository = Substitute.For<ICarRepository>();
        private readonly ICarSpecsRepository _carSpecsRepository = Substitute.For<ICarSpecsRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly AddCarDetailsCommandHandler _sut;

        public AddCarDetailsTests()
        {
            _sut = new AddCarDetailsCommandHandler(_unitOfWork, _carRepository, _carSpecsRepository);
        }

        [Fact]
        public async Task CarIdNotFound_ShouldFail()
        {
            // Arrange
            var command = new AddCarDetailsCommand(Guid.NewGuid(), CarBodyStyle.Sedan, 5.0, EngineType.Diesel, 2000, 4, 200, TransmissionType.Auto, 6, 200, 6.0, true, 4);
            _carRepository.GetById(command.CarId).ReturnsNull();
            // Act
            var result = await _sut.Handle(command, default);
            // Assert
            Assert.True(result.IsFailed);
            Assert.True(result.Error!.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CarSpecsNotFound_AddNewSpecs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AddCarDetailsCommand(id, CarBodyStyle.Sedan, 5.0, EngineType.Diesel, 2000, 4, 200, TransmissionType.Auto, 6, 200, 6.0, true, 4);
            _carRepository.GetById(command.CarId).Returns(new Car() { Id = id });
            _carSpecsRepository.GetById(command.CarId).ReturnsNull();
            // Act
            var result = await _sut.Handle(command, default);
            // Assert
            _carSpecsRepository.Received(1).Add(Arg.Any<CarSpecs>());
            await _unitOfWork.Received(1).SaveChangesAsync();
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CarSpecsFound_UpdateCurrentSpecs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new AddCarDetailsCommand(id, CarBodyStyle.Sedan, 5.0, EngineType.Diesel, 2000, 4, 200, TransmissionType.Auto, 6, 200, 6.0, true, 4);
            _carRepository.GetById(command.CarId).Returns(new Car() { Id = id });
            _carSpecsRepository.GetById(command.CarId).Returns(new CarSpecs());
            // Act
            var result = await _sut.Handle(command, default);
            // Assert
            _carSpecsRepository.Received(1).Update(Arg.Any<CarSpecs>());
            await _unitOfWork.Received(1).SaveChangesAsync();
            Assert.True(result.IsSuccess);
        }
    }
}