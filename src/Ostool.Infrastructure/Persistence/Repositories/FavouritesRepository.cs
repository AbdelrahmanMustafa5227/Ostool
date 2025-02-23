using Microsoft.EntityFrameworkCore;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Advertisements.Responses;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Infrastructure.Persistence.Repositories
{
    internal class FavouritesRepository : IFavouritesRepository
    {
        private readonly AppDbContext _appDbContext;

        public FavouritesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Favourites favourites)
        {
            _appDbContext.Favourites.Add(favourites);
        }

        public void Delete(Favourites favourites)
        {
            _appDbContext.Favourites.Remove(favourites);
        }

        public async Task<Favourites?> GetById(Guid id)
        {
            return await _appDbContext.Favourites.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AdvertisementResponse>> GetAllSavedAdsByVisitorId(Guid visitorId)
        {
            return await _appDbContext.Favourites
                .Where(x => x.VisitorId == visitorId)
                .Select(x => new AdvertisementResponse(
                    x.AdvertisementId,
                    x.Advertisement.Car.Brand,
                    x.Advertisement.Car.Model,
                    x.Advertisement.Vendor.VendorName,
                    x.Advertisement.Price,
                    x.Advertisement.Year
                ))
                .ToListAsync();
        }
    }
}