namespace Basis.Biblioteca.Application.Common.DTOs;

public class PagedResultDto<T>(long total, IEnumerable<T> items)
{
    public long Total { get; set; } = total;

    public int Count { get; set; } = items.Count();

    public IEnumerable<T> Items { get; set; } = items;
}
