using Ostool.Domain.Enums;

namespace Ostool.Application.Features.Advertisements.Responses
{
    public record AdvertisementDetailedResponse(
        Guid Id,
        string Model,
        string Brand,
        CarBodyStyle BodyStyle,
        double GroundClearance,
        EngineType EngineType,
        int Displacement,
        int Horsepower,
        int NumberOfCylinders,
        TransmissionType TransmissionType,
        int NumberOfGears,
        double TopSpeed,
        double ZeroToSixty,
        bool HasSunRoof,
        int SeatingCapacity,
        string VendorName,
        string ContactNumber,
        string Email,
        string? Description,
        decimal Price,
        int Year,
        DateTime PostedDate);

}