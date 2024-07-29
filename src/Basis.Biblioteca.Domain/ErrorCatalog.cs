using ErrorOr;

namespace Basis.Biblioteca.Domain;

public static class ErrorCatalog
{
    public static Error NotFound
        => Error.NotFound("ERR-CODE-01", "Registro não encontrado");


}
