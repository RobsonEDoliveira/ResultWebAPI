using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Interfaces.Services
{
    // INTERFACE USUÁRIO SERVICE
    public interface IUsuarioService
    {
        Task<Response<bool>> AutenticarUsuario(string password, UsuarioModel user);
        Task<Response> CriarUsuario(UsuarioModel user);
        Task<Response> AtualizarUsuario(UsuarioModel user);
        Task<Response> DeletarUsuario(string userId);
        Task<Response<UsuarioModel>> RetornarUsuarioID(string userId);
        Task<Response<UsuarioModel>> RetornarUsuarioLOGIN(string login);
        Task<Response<List<UsuarioModel>>> ListarUsuarioPorFILTRO(string userId = null, string nome = null);
    }
}
