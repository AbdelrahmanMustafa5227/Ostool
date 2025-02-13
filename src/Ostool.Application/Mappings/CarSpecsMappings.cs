using Ostool.Application.Features.Cars.AddCar;
using Ostool.Application.Features.CarSpecs.AddCarDetails;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Mappings
{
    public static class CarSpecsMappings
    {
        public static CarSpecs ToModel(this AddCarDetailsCommand command)
        {
            return new CarSpecs
            {
                CarId = command.CarId,
                BodyStyle = command.BodyStyle,
                GroundClearance = command.GroundClearance,
                EngineType = command.EngineType,
                Displacement = command.Displacement,
                Horsepower = command.HorsePower,
                NumberOfCylinders = command.numOfCylinders,
                TransmissionType = command.TransmissionType,
                NumberOfGears = command.numOfGears,
                TopSpeed = command.TopSpeed,
                ZeroToSixty = command.ZeroToSixty,
                HasSunRoof = command.HasSumRoof,
                SeatingCapacity = command.SeatingCapacity,
            };
        }

        public static CarSpecs ApplyChanges(this CarSpecs carSpecs, AddCarDetailsCommand command)
        {
            carSpecs.BodyStyle = command.BodyStyle;
            carSpecs.GroundClearance = command.GroundClearance;
            carSpecs.EngineType = command.EngineType;
            carSpecs.Displacement = command.Displacement;
            carSpecs.Horsepower = command.HorsePower;
            carSpecs.NumberOfCylinders = command.numOfCylinders;
            carSpecs.TransmissionType = command.TransmissionType;
            carSpecs.NumberOfGears = command.numOfGears;
            carSpecs.TopSpeed = command.TopSpeed;
            carSpecs.ZeroToSixty = command.ZeroToSixty;
            carSpecs.HasSunRoof = command.HasSumRoof;
            carSpecs.SeatingCapacity = command.SeatingCapacity;
            return carSpecs;
        }
    }
}