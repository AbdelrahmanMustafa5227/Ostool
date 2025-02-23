using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Vendors.UpdateVendor;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.UnitTests.VendorTests
{
    public class UpdateVendorTests
    {
        private readonly IVendorRepository _vendorRepository = Substitute.For<IVendorRepository>();
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly UpdateVendorCommandHandler _sut;

        public UpdateVendorTests()
        {
            _sut = new UpdateVendorCommandHandler(_vendorRepository, _unitOfWork);
        }

        [Fact]
        public async Task WhenVendorIsNotFound_ShouldFail()
        {
            // Arrange
            var vendorId = Guid.NewGuid();
            _vendorRepository.GetById(vendorId).ReturnsNull();
            // Act
            var result = await _sut.Handle(new UpdateVendorCommand(vendorId, "Name", "ContactNumber", "Email"), default);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Error!.StatusCode);
        }

        [Fact]
        public async Task WhenVendorIsFound_ShouldUpdate()
        {
            // Arrange
            var vendorId = Guid.NewGuid();
            var vendor = new Vendor { Id = vendorId };
            _vendorRepository.GetById(vendorId).Returns(vendor);
            // Act
            var result = await _sut.Handle(new UpdateVendorCommand(vendorId, "Name", "ContactNumber", "Email"), default);
            // Assert
            Assert.True(result.IsSuccess);
            _vendorRepository.Received(1).Update(vendor);
            await _unitOfWork.Received(1).SaveChangesAsync();
        }
    }
}