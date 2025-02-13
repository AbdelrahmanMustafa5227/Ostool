using MediatR;
using NSubstitute;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Caching.Vendors;
using Ostool.Application.Features.Vendors.AddVendor;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.VendorTests
{
    public class AddVendorTests
    {
        private readonly IVendorRepository _vendorRepository = Substitute.For<IVendorRepository>();
        private readonly IPublisher _publisher = Substitute.For<IPublisher>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly AddVendorCommandHandler _sut;

        public AddVendorTests()
        {
            _sut = new AddVendorCommandHandler(_vendorRepository, _unitOfWork, _publisher);
        }

        [Fact]
        public async Task EmailAlreadyExists_ShouldFail()
        {
            // Arrange
            var command = new AddVendorCommand("Vendor Name", "1234567890", "test@test");
            _vendorRepository.Exists(command.Email).Returns(true);
            // Act
            var result = await _sut.Handle(command, default);
            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(HttpStatusCode.Conflict, result.Error!.StatusCode);
        }

        [Fact]
        public async Task EmailDoesNotExists_ShouldSucceed()
        {
            // Arrange
            var command = new AddVendorCommand("Vendor Name", "1234567890", "test@test");
            _vendorRepository.Exists(command.Email).Returns(false);
            // Act
            var result = await _sut.Handle(command, default);
            // Assert
            _vendorRepository.Received(1).Add(Arg.Any<Vendor>());
            await _unitOfWork.Received(1).SaveChangesAsync();
            await _publisher.Received(1).Publish(Arg.Any<VendorsCacheInvalidationEvent>());
            Assert.True(result.IsSuccess);
        }
    }
}