using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Vendors;
using Ostool.Application.Features.Vendors.DeleteVendor;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.VendorTests
{
    public class DeleteVendorTests
    {
        private readonly IVendorRepository _vendorRepository = Substitute.For<IVendorRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly IPublisher _publisher = Substitute.For<IPublisher>();
        private readonly DeleteVendorCommandHandler _sut;

        public DeleteVendorTests()
        {
            _sut = new DeleteVendorCommandHandler(_vendorRepository, _unitOfWork, _publisher);
        }

        [Fact]
        public async Task WhenVendorIsNotFound_ShouldFail()
        {
            // Arrange
            var vendorId = Guid.NewGuid();
            _vendorRepository.GetById(vendorId).ReturnsNull();
            // Act
            var result = await _sut.Handle(new DeleteVendorCommand(vendorId), default);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
        }

        [Fact]
        public async Task VendorIdIsFound_ShouldSucceed()
        {
            // Arrange
            var vendorId = Guid.NewGuid();
            var vendor = new Vendor { Id = vendorId };
            _vendorRepository.GetById(vendorId).Returns(vendor);
            // Act
            var result = await _sut.Handle(new DeleteVendorCommand(vendorId), default);
            // Assert
            _vendorRepository.Received(1).Delete(vendor);
            await _unitOfWork.Received(1).SaveChangesAsync();
            await _publisher.Received().Publish(Arg.Any<VendorsCacheInvalidationEvent>());
            Assert.True(result.IsSuccess);
        }
    }
}