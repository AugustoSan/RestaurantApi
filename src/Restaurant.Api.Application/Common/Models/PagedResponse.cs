namespace Restaurant.Api.Application.Common.Models;

public class PagedResponse<T>
{
    public List<T> Items { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
    public int? NextPage => HasNext ? CurrentPage + 1 : null;
    public int? PreviousPage => HasPrevious ? CurrentPage - 1 : null;

    // Constructor que maneja la paginación
    public PagedResponse(IEnumerable<T> source, int pageSize, int currentPage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        TotalCount = source.Count();
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        
        Items = source
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    // Constructor alternativo
    public PagedResponse(IEnumerable<T> items, int pageSize, int currentPage, int totalCount)
    {
        Items = items.ToList();
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
