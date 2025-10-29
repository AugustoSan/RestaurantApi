using MediatR;

namespace Restaurant.Api.Application.Common.Models;

public abstract class PagedQueryOptionsBase<TResult> : IRequest<PagedResponse<TResult>>
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;
    
    private int _currentPage = DefaultPage;
    private int _pageSize = DefaultPageSize;

    public string? Search { get; set; }
    public int CurrentPage { get => _currentPage; set => _currentPage = value <= 0 ? DefaultPage : value; }
    public int PageSize { get => _pageSize; set => _pageSize = value <= 0 ? DefaultPageSize : value; }
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; }
}