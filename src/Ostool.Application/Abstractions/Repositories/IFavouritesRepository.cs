using Ostool.Application.Features.Advertisements.Responses;
using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IFavouritesRepository
    {
        void Add(Favourites favourites);
        void Delete(Favourites favourites);
        Task<Favourites?> GetById(Guid id);
        Task<List<AdvertisementResponse>> GetAllSavedAdsByVisitorId(Guid visitorId);
    }
}
