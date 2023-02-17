using FluentValidation;
using Donus.Domain.Models;

namespace Donus.Domain.Validations
{
    public class MailingValidation : AbstractValidator<MailingModel>
    {
        // VALIDAÇÃO
        public MailingValidation()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .Length(3, 30);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Telefone)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.Logradouro)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.Estado)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.Cidade)
               .NotNull()
               .NotEmpty();

            RuleFor(x => x.Genero)
               .NotNull()
               .NotEmpty();
        }
    }
}
