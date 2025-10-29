using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Common.Models;

namespace Restaurant.Api.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : PagedQueryOptionsBase<CategoryDto>
    {
        
    }
}