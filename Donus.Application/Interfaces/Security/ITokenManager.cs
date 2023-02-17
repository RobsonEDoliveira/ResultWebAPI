using Donus.Application.DataContract.Response.Usuario;
using Donus.Domain.Models;
using System.Threading.Tasks;

namespace Donus.Application.Interfaces.Security
{
    public interface ITokenManager
    {
        Task<AuthResponse> GenerateTokenAsync(UsuarioModel user);
    }
}
