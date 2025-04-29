using FluentValidation;

namespace ConcesionarioApi.DTOs.Validators
{
    public class UpdateAutoDtoValidator : AbstractValidator<UpdateAutoDto>
    {
        public UpdateAutoDtoValidator() 
        {
            Include(new CreateAutoDtoValidator()); //con esto heredas reglas
        }
    }
}
