using Donus.Application.DataContract.Request.Usuario;
using Donus.Application.DataContract.Response.Usuario;
using Donus.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Application.Interfaces
{
    // INTERFACE USUÁRIO APPLICATION
    public interface IUsuarioApplication
    {
        Task<Response<AuthResponse>> AuthAsync(AuthRequest auth);
        Task<Response> CriarUsuario(CreateUsuarioRequest User);
        Task<Response<List<UsuarioResponse>>> ListarUsuarioPorFILTRO(string userId = null, string nome = null);
    }
}
