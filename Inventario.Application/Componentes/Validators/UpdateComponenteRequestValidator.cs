using FluentValidation;
using Inventario.Application.Componentes.Dtos;

namespace Inventario.Application.Componentes.Validators;

public class UpdateComponenteRequestValidator : AbstractValidator<UpdateComponenteRequest>
{
    public UpdateComponenteRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().MaximumLength(150);

        RuleFor(x => x.Tipo)
            .NotEmpty().MaximumLength(100);
    }
}
