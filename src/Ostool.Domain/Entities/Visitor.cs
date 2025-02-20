using Ostool.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Domain.Entities
{
    public class Visitor : AppUser, IEntity
    {
        public bool SubscribedToNewsletter { get; set; }
    }
}