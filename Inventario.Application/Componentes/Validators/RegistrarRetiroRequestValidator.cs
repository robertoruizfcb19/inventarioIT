using FluentValidation;
using Inventario.Application.Componentes.Dtos;

namespace Inventario.Application.Componentes.Validators;

public class RegistrarRetiroRequestValidator : AbstractValidator<RegistrarRetiroRequest>
{
    public RegistrarRetiroRequestValidator()
    {
        RuleFor(x => x.FechaRetiro).LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));
    }
}
