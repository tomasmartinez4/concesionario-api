using FluentValidation;

namespace ConcesionarioApi.DTOs.Validators
{
    public class CreateAutoDtoValidator : AbstractValidator<CreateAutoDto>
    {
        public CreateAutoDtoValidator() 
        {
            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("La marca es obligatoria")
                .MaximumLength(100);
            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("El modelo es obligatorio")
                .MaximumLength(100);
            RuleFor(x => x.Anio)
                .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .WithMessage("El año debe estar entre 1900 y el siguiente año");
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0");
        }
    }
}
