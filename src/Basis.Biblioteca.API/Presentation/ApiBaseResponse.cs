using ErrorOr;

namespace Basis.Biblioteca.API.Presentation;

public class ApiBaseResponse<T>
{
    public int Status { get; set; }
    public T? Data { get; set; }
    public List<Error> Errors { get; set; } = [];
}
