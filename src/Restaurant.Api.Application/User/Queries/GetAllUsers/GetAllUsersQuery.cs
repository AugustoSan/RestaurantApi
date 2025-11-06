using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Application.Common.Models;

namespace Restaurant.Api.Application.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery : PagedQueryOptionsBase<UserDto>
    {
        
    }
}