using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using Ostool.Application.Helpers;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly AppDbContext _appDbContext;

        public AdvertisementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(Advertisement advertisement)
        {

            _appDbContext.Advertisements.Add(advertisement);
        }

        public async Task<QueryResult<AdvertisementResponse>> GetAll(int pageNumber)
        {
            var totalItems = await _appDbContext.Advertisements.CountAsync();

            var ads = await _appDbContext
                .Advertisements
                .OrderBy(x => x.PostedDate)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.VendorName, x.Price, x.Year))
                .ToListAsync();

            return new QueryResult<AdvertisementResponse>(ads, totalItems);
        }

        public async Task<QueryResult<AdvertisementResponse>> GetAllByCarModel(string carModel, int pageNumber)
        {
            var totalItems = await _appDbContext.Advertisements.CountAsync(x => x.Car.Model == carModel);

            var ads = await _appDbContext
                .Advertisements
                .OrderBy(x => x.PostedDate)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Where(x => x.Car.Model == carModel)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.VendorName, x.Price, x.Year))
                .ToListAsync();

            return new QueryResult<AdvertisementResponse>(ads, totalItems);
        }

        public async Task<QueryResult<AdvertisementResponse>> GetAllByVendor(string vendorName, int pageNumber)
        {
            var totalItems = await _appDbContext.Advertisements.CountAsync(x => x.Vendor.VendorName == vendorName);

            var ads = await _appDbContext
                .Advertisements
                .Where(x => x.Vendor.VendorName == vendorName)
                .OrderBy(x => x.PostedDate)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.VendorName, x.Price, x.Year))
                .ToListAsync();

            return new QueryResult<AdvertisementResponse>(ads, totalItems);
        }

        public async Task<AdvertisementDetailedResponse?> GetById(Guid id)
        {
            var query =
                """
                WITH cte AS
                (
                    SELECT a.Id, c.Id AS CarID , c.Brand , c.Model , v.VendorName , v.ContactNumber , v.Email , a.Description , a.Price , a.Year , a.PostedDate
                    FROM Advertisements a 
                    INNER JOIN Cars c ON a.CarId = c.Id
                    INNER JOIN Vendors v ON a.VendorId = v.Id
                    WHERE a.Id = @id
                )
                SELECT cte.Id , cte.Model, cte.Brand ,sp.BodyStyle , sp.GroundClearance, sp.EngineType , sp.Displacement , sp.Horsepower , sp.NumberOfCylinders,
                	sp.TransmissionType , sp.NumberOfGears , sp.TopSpeed , sp.ZeroToSixty , sp.HasSunRoof , sp.SeatingCapacity , cte.VendorName , cte.ContactNumber,
                	cte.Email , cte.Description , cte.Price , cte.Year , cte.PostedDate
                FROM cte
                Inner Join CarSpecs sp ON cte.CarID = sp.CarId
                """;

            var ad = (await _appDbContext.Database.SqlQueryRaw<AdvertisementDetailedResponse>(query, new SqlParameter("@id", id))
                .ToListAsync())
                .FirstOrDefault();

            return ad;
        }

        public void Update(Advertisement advertisement)
        {
            _appDbContext.Advertisements.Update(advertisement);
        }
    }
}