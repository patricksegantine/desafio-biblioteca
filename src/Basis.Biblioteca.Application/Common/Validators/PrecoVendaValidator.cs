using Basis.Biblioteca.Application.Common.DTOs;
using FluentValidation;

namespace Basis.Biblioteca.Application.Common.Validators;

public class PrecoVendaValidator : AbstractValidator<PrecoVendaDto>
{
    public PrecoVendaValidator(bool isUpdating)
    {
        if (isUpdating)
        {
            RuleFor(p => p.CodP)
                .GreaterThan(0);
        }
        else
        {
            RuleFor(p => p.CodP)
                .Null();
        }

        RuleFor(p => p.TipoDeVenda)
            .IsInEnum();

        RuleFor(p => p.Preco)
            .GreaterThanOrEqualTo(0);
    }
}