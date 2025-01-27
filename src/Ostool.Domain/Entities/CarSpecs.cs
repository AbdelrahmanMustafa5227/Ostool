using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ostool.Domain.Entities
{
    public class CarSpecs
    {
        // Dimensions
        public CarBodyStyle BodyStyle { get; set; }  // Sedan, coupe, SUV, hatchback
        public double Length { get; set; }
        public double Width { get; set; }
        public double Heigth { get; set; }
        public double WheelBase { get; set; } // Distance between the front and rear axles
        public double GroundClearance { get; set; } // Distance from the ground to the lowest point on the vehicle

        // Engine
        public EngineType EngineType { get; set; } // Gasoline, Diesel, Electric
        public int Displacement { get; set; } // Measured in liters or cubic centimeters (cc)
        public int Horsepower { get; set; } //Measured in horsepower (hp)
        public int Torque { get; set; } // Measured in Newton-meters (Nm)
        public int NumberOfCylinders { get; set; }

        // Transmission
        public TransmissionType TransmissionType { get; set; } // Manual, Automatic, CVT
        public int NumberOfGears { get; set; }

        // Performance
        public double TopSpeed { get; set; }  // in km/h
        public double ZeroToSixty { get; set; } // Acceleration time from 0 to 60 km per hour

        //Features
        public bool HasSunRoof { get; set; }
        public int SeatingCapacity { get; set; }


        public Car Car { get; set; } = new();
        public int CarId { get; set; }
    }
}
