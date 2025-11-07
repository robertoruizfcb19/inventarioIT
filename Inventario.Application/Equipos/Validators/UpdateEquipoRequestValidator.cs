using FluentValidation;
using Inventario.Application.Equipos.Dtos;

namespace Inventario.Application.Equipos.Validators;

public class UpdateEquipoRequestValidator : AbstractValidator<UpdateEquipoRequest>
{
    public UpdateEquipoRequestValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().MaximumLength(200);

        RuleFor(x => x.Categoria)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.FechaAdquisicion)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));

        RuleFor(x => x.ValorCompra)
            .GreaterThanOrEqualTo(0).When(x => x.ValorCompra.HasValue);
    }
}
