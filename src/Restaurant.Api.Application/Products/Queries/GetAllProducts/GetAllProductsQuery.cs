using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Restaurant.Api.Application.Products.Dtos;

namespace Restaurant.Api.Application.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
        
    }
}