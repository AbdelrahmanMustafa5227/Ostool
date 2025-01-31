using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Caching.Cars
{
    public record CarCacheInvalidationOnAddOrDeleteEvent(string Brand) : INotification;

}