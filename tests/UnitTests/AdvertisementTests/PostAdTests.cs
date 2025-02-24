using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.PostAd;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.AdvertisementTests
{
    public class PostAdTests
    {
        /*private readonly IAdvertisementRepository _advertisementRepository = Substitute.For<IAdvertisementRepository>();
        private readonly ICarRepository _carRepository = Substitute.For<ICarRepository>();
        private readonly IVendorRepository _vendorRepository = Substitute.For<IVendorRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly PostAdCommandHandler _sut;

        public PostAdTests()
        {
            _sut = new PostAdCommandHandler(_advertisementRepository, _unitOfWork, _carRepository, _vendorRepository);
        }

        [Fact]
        public async Task CarIdNotFound_ShouldReturnNotFoundError()
        {
            // Arrange
            var request = new PostAdCommand(Guid.NewGuid(), Guid.NewGuid(), "Description", 1000, 2022, DateTime.Now.AddDays(30));
            _carRepository.GetById(request.CarId).ReturnsNull();
            // Act
            var result = await _sut.Handle(request, CancellationToken.None);
            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
        }

        [Fact]
        public async Task VendorIdNotFound_ShouldReturnNotFoundError()
        {
            // Arrange
            var request = new PostAdCommand(Guid.NewGuid(), Guid.NewGuid(), "Description", 1000, 2022, DateTime.Now.AddDays(30));
            _carRepository.GetById(request.CarId).Returns(new Car());
            _vendorRepository.GetById(request.VendorId).ReturnsNull();
            // Act
            var result = await _sut.Handle(request, CancellationToken.None);
            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
        }
        [Fact]
        public async Task CarAndVendorAreFound_ShouldAddAnAdvertisement()
        {
            // Arrange
            var request = new PostAdCommand(Guid.NewGuid(), Guid.NewGuid(), "Description", 1000, 2022, DateTime.Now.AddDays(30));
            _carRepository.GetById(request.CarId).Returns(new Car());
            _vendorRepository.GetById(request.VendorId).Returns(new Vendor());
            // Act
            var result = await _sut.Handle(request, CancellationToken.None);
            // Assert
            _advertisementRepository.Received(1).Add(Arg.Any<Advertisement>());
            await _unitOfWork.Received(1).SaveChangesAsync();
            Assert.True(result.IsSuccess);
        }*/
    }
}