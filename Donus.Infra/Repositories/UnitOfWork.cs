using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Repositories.DataConnector;

namespace Donus.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUsuarioRepository _UsuarioRepository;
        private IMailingRepository _mailingRepository;

        public UnitOfWork(IDbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        // MAILING
        public IMailingRepository MailingRepository => _mailingRepository ?? (_mailingRepository = new MailingRepository(dbConnector));

        public IUsuarioRepository UsuarioRepository => _UsuarioRepository ?? (_UsuarioRepository = new UsuarioRepository(dbConnector));

        public IDbConnector dbConnector { get; }

        public void BeginTransaction()
        {
            dbConnector.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        // COMITAR TRANSAÇÃO
        public void CommitTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Commit();
            }
        }

        // ROLLBACK DA TRANSAÇÃO
        public void RollbackTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Rollback();
            }
        }
    }
}
