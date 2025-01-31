using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Cache
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}