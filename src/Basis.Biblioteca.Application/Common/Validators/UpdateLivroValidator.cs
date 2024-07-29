using Basis.Biblioteca.Application.UseCases.Livro.Update;
using FluentValidation;

namespace Basis.Biblioteca.Application.Common.Validators;

public class UpdateLivroValidator : AbstractValidator<UpdateLivroRequest>
{
    public UpdateLivroValidator()
    {
        RuleFor(l => l.Titulo)
            .Length(5, 40)
            .When(l => !string.IsNullOrWhiteSpace(l.Titulo));

        RuleFor(l => l.Editora)
            .Length(5, 40)
            .When(l => !string.IsNullOrWhiteSpace(l.Editora));

        RuleFor(l => l.Edicao)
            .GreaterThan(0)
            .When(l => l.Edicao.HasValue);

        RuleFor(l => l.AnoPublicacao)
            .Length(4)
            .When(l => !string.IsNullOrWhiteSpace(l.AnoPublicacao));
    }
}