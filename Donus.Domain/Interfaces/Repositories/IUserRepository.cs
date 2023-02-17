using Donus.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task CriarUsuario(UsuarioModel user);
        Task AtualizarUsuario(UsuarioModel user);
        Task DeletarUsuario(string userId);
        Task<UsuarioModel> RetornarUsuarioID(string userId);
        Task<UsuarioModel> RetornarUsuarioLOGIN(string login);
        Task<List<UsuarioModel>> ListarUsuarioPorFILTRO(string userId = null, string nome = null);
        Task<bool> VerificarUsuarioEXISTENTE(string userId);
        Task<bool> VerificarLoginEXISTENTE(string login);
    }
}
