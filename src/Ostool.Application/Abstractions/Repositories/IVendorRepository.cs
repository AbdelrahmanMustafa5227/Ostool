﻿using Ostool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Abstractions.Repositories
{
    public interface IVendorRepository
    {
        void Add(Vendor vendor);
        void Update(Vendor vendor);
    }
}