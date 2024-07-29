using ErrorOr;
using FluentValidation.Results;

namespace Basis.Biblioteca.Application.Common.Extensions;

public static class ErrorOrExtensions
{
    public static List<Error> ToErrorList(this List<ValidationFailure> failures)
    {
        return failures.ConvertAll((ValidationFailure failure) => Error.Custom((int)ErrorType.Validation, failure.PropertyName ?? failure.ErrorCode, failure.ErrorMessage)) ?? Enumerable.Empty<Error>().ToList();
    }
}
