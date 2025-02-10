namespace Ostool.Application.Features.Advertisements.Responses
{
    public record AdvertisementResponse(Guid Id, string Model, string Brand, string VendorName, decimal Price, int year);

}