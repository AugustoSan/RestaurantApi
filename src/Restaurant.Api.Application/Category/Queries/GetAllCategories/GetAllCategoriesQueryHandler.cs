using MediatR;
using Restaurant.Api.Application.Categories.Queries.GetAllCategories;
using Restaurant.Api.Application.Category.Dtos;
using Restaurant.Api.Application.Category.Mapper;
using Restaurant.Api.Application.Common.Models;
using Restaurant.Api.Core.Interfaces;

namespace Restaurant.Api.Application.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository
) : IRequestHandler<GetAllCategoriesQuery, PagedResponse<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<PagedResponse<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategories();
        var categoryDtos = categories.Select(CategoryMapper.ToDto).ToList();
        return new PagedResponse<CategoryDto>(
            source: categoryDtos, 
            pageSize: request.PageSize, 
            currentPage: request.CurrentPage
        );
    }
}
