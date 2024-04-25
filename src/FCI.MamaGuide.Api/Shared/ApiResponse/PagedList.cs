namespace FCI.MamaGuide.Api.Shared.ApiResponse;

public class PagedList<T> : List<T> where T : class
{
    public MetaData MetaData { get; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        AddRange(items);
    }
}