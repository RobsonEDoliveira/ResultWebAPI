using Microsoft.Extensions.DependencyInjection;
using Donus.Application.Applications;
using Donus.Application.Interfaces;
using Donus.Application.Interfaces.Security;
using Donus.Application.Security;
using Donus.Domain.Common;
using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Services;
using Donus.Domain.Services;
using Donus.Infra.Repositories;

namespace Donus.Api.Extensions
{
    public static class RegisterIoCExtensions
    {
        public static void RegisterIoC(this IServiceCollection services)
        {
            // UNIT
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITimeProvider, TimeProvider>();
            services.AddScoped<IGenerators, Generators>();

            // MAILING
            services.AddScoped<IMailingApplication, MailingApplication>();
            services.AddScoped<IMailingService, MailingService>();
            services.AddScoped<IMailingRepository, MailingRepository>();

            //USUÁRIO
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // SEGURANÇA
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ITokenManager, TokenManager>();
        }
    }
}
