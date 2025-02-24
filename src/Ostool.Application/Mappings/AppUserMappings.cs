using MediatR;
using Ostool.Application.Abstractions.Repositories;
using Ostool.Application.Features.Auth.LocalRegister;
using Ostool.Domain.Entities;
using Ostool.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.Application.Mappings
{
    public static class AppUserMappings
    {
        public static AppUser ToAppUser(this LocalRegisterCommand command)
        {
            if (command.role == 0)
            {
                return new Visitor
                {
                    Email = command.Email,
                    UserName = command.UserName,
                    Password = command.Password,
                    Id = Guid.NewGuid(),
                    SubscribedToEmails = false,
                    AuthProvider = AuthProvider.Local
                };
            }

            return new Vendor
            {
                Email = command.Email,
                Password = command.Password,
                UserName = command.UserName,
                Id = Guid.NewGuid(),
                AuthProvider = AuthProvider.Local
            };
        }
    }
}