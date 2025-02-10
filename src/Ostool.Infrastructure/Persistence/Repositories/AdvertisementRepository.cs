using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
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

        public async Task<List<AdvertisementResponse>> GetAll()
        {
            return await _appDbContext
                .Advertisements
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.Name, x.Price, x.Year))
                .ToListAsync();
        }

        public async Task<List<AdvertisementResponse>> GetAllByCarModel(string carModel)
        {
            return await _appDbContext
                .Advertisements
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Where(x => x.Car.Model == carModel)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.Name, x.Price, x.Year))
                .ToListAsync();
        }

        public async Task<List<AdvertisementResponse>> GetAllByVendor(string vendorName)
        {
            return await _appDbContext
                .Advertisements
                .Include(x => x.Car)
                .Include(x => x.Vendor)
                .Where(x => x.Vendor.Name == vendorName)
                .Select(x => new AdvertisementResponse(x.Id, x.Car.Brand, x.Car.Model, x.Vendor.Name, x.Price, x.Year))
                .ToListAsync();
        }

        public async Task<AdvertisementDetailedResponse?> GetById(Guid id)
        {
            var query =
                """
                WITH cte AS
                (
                    SELECT a.Id, c.Id AS CarID , c.Brand , c.Model , v.Name , v.ContactNumber , v.Email , a.Description , a.Price , a.Year , a.PostedDate
                    FROM Advertisements a 
                    INNER JOIN Cars c ON a.CarId = c.Id
                    INNER JOIN Vendors v ON a.VendorId = v.Id
                    WHERE a.Id = @id
                )
                SELECT cte.Id , cte.Model, cte.Brand ,sp.BodyStyle , sp.GroundClearance, sp.EngineType , sp.Displacement , sp.Horsepower , sp.NumberOfCylinders,
                	sp.TransmissionType , sp.NumberOfGears , sp.TopSpeed , sp.ZeroToSixty , sp.HasSunRoof , sp.SeatingCapacity , cte.Name AS VendorName , cte.ContactNumber,
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