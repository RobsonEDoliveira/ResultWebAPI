using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Donus.Domain.Interfaces.Services
{
    // INTERFACES SEGURANÇA
    public interface ISecurityService
    {
        Task<Response<bool>> CompararPassword(string password, string confirmPassword);
        Task<Response<bool>> ValidarPassword(string password, UsuarioModel user);
        Task<Response<string>> CryptografarPassword(string password);
    }
}
