using Donus.Domain.Interfaces.Repositories.DataConnector;

namespace Donus.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }

        IDbConnector dbConnector { get;}

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
