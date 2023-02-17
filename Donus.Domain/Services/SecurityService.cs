using Donus.Domain.Interfaces.Services;
using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Donus.Domain.Services
{
    public class SecurityService : ISecurityService
    {
        // COMPARAR PASSWORD
        public Task<Response<bool>> CompararPassword(string password, string confirmPassword)
        {
            var isEquals = password.Trim().Equals(confirmPassword.Trim());

            return Task.FromResult(Response.OK<bool>(isEquals));
        }

        public Task<Response<string>> CryptografarPassword(string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return Task.FromResult(Response.OK<string>(passwordHash));
        }

        public Task<Response<bool>> ValidarPassword(string password, UsuarioModel user)
        {
            bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return Task.FromResult(Response.OK<bool>(validPassword));
        }
    }
}
