using MediatR;
using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Features.Cars.GetByBrand;
using Ostool.Application.Features.Cars.UpdateCar;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Mappings
{
    internal static class CarMappings
    {
        public static Car ToModel(this AddCarCommand command)
        {
            return new Car
            {
                Id = Guid.NewGuid(),
                Model = command.Model,
                Brand = command.Brand,
                AvgPrice = command.AvgPrice,
            };
        }

        public static GetCarByBrandResponse ToDto(this Car command)
        {
            return new GetCarByBrandResponse(command.Brand, command.Model, command.AvgPrice);
        }

        public static Car ToModel(this UpdateCarCommand command)
        {
            return new Car
            {
                Id = command.Id,
                Model = command.Model,
                Brand = command.Brand,
                AvgPrice = command.AvgPrice,
            };
        }

        public static Car ApplyChanges(this Car car, UpdateCarCommand command)
        {
            car.Model = command.Model;
            car.Brand = command.Brand;
            car.AvgPrice = command.AvgPrice;
            return car;
        }
    }
}