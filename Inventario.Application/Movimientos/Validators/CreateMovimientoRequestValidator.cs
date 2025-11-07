using FluentValidation;
using Inventario.Application.Movimientos.Dtos;

namespace Inventario.Application.Movimientos.Validators;

public class CreateMovimientoRequestValidator : AbstractValidator<CreateMovimientoRequest>
{
    public CreateMovimientoRequestValidator()
    {
        RuleFor(x => x.ComponenteId)
            .NotEmpty();

        RuleFor(x => x.FechaMovimiento)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));
    }
}
