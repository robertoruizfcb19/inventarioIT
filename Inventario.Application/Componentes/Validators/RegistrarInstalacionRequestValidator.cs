using FluentValidation;
using Inventario.Application.Componentes.Dtos;

namespace Inventario.Application.Componentes.Validators;

public class RegistrarInstalacionRequestValidator : AbstractValidator<RegistrarInstalacionRequest>
{
    public RegistrarInstalacionRequestValidator()
    {
        RuleFor(x => x.EquipoId).NotEmpty();
        RuleFor(x => x.FechaInstalacion).LessThanOrEqualTo(DateTime.UtcNow.AddDays(1));
    }
}
