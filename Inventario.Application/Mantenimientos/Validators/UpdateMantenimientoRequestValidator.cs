using FluentValidation;
using Inventario.Application.Mantenimientos.Dtos;

namespace Inventario.Application.Mantenimientos.Validators;

public class UpdateMantenimientoRequestValidator : AbstractValidator<UpdateMantenimientoRequest>
{
    public UpdateMantenimientoRequestValidator()
    {
        RuleFor(x => x.FechaProgramada)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(1));

        RuleFor(x => x.Costo)
            .GreaterThanOrEqualTo(0).When(x => x.Costo.HasValue);
    }
}
