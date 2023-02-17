using FluentValidation;
using Donus.Domain.Models;

namespace Donus.Domain.Validations
{
    public class UserValidation : AbstractValidator<UsuarioModel>
    {
        // VALIDAÇÃO
        public UserValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .Length(3, 30);

            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PasswordHash)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
