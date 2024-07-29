using Basis.Biblioteca.Application.UseCases.Livro.Create;
using FluentValidation;

namespace Basis.Biblioteca.Application.Common.Validators;

public class CreateLivroValidator : AbstractValidator<CreateLivroRequest>
{
    public CreateLivroValidator()
    {
        RuleFor(l => l.Titulo)
            .NotEmpty()
            .Length(5, 40);

        RuleFor(l => l.Editora)
            .NotEmpty()
            .Length(5, 40);

        RuleFor(l => l.Edicao)
            .GreaterThan(0);

        RuleFor(l => l.AnoPublicacao)
            .NotEmpty()
            .Length(4);

        RuleFor(l => l.Autores)
            .NotEmpty();

        RuleFor(l => l.Assuntos)
            .NotEmpty();

        RuleFor(l => l.Precos)
            .ForEach(l => l.SetValidator(new PrecoVendaValidator(false)));
    }
}
