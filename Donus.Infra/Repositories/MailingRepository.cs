using Dapper;
using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Repositories.DataConnector;
using Donus.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Infra.Repositories
{
    public class MailingRepository : IMailingRepository
    {
        private readonly IDbConnector _dbConnector;
        public MailingRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        // CONSTANTE BASE
        const string baseSql = @"SELECT [Id]
                                       ,[Nome]
                                       ,[Email]
                                       ,[Telefone]
                                       ,[Logradouro]
                                       ,[Estado]
                                       ,[Cidade]
                                       ,[Genero]
                                       ,[DataCadastro]
                                   FROM [dbo].[Mailing]
                                   WHERE 1 = 1 ";

        // CRIAR NOVO MAILING
        public async Task CriarMailing(MailingModel mailing)
        {
            string sql = @"INSERT INTO [dbo].[Mailing]
                                ([Id]
                                ,[Nome]
                                ,[Email]
                                ,[Telefone]
                                ,[Logradouro]
                                ,[Estado]
                                ,[Cidade]
                                ,[Genero]
                                ,[DataCadastro])
                          VALUES
                                (@Id
                                ,@Nome
                                ,@Email
                                ,@Telefone
                                ,@Logradouro
                                ,@Estado
                                ,@Cidade
                                ,@Genero
                                ,@DataCadastro)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = mailing.Id,
                Nome = mailing.Nome,
                Email = mailing.Email,
                Telefone = mailing.Telefone,
                Logradouro = mailing.Logradouro,
                Estado = mailing.Estado,
                Cidade = mailing.Cidade,
                Genero = mailing.Genero,
                DataCadastro = mailing.DataCadastro
            }, _dbConnector.dbTransaction);
        }

        // ATUALIZAR/ALTERAR MAILING
        public async Task AtualizarMailing(MailingModel mailing)
        {
            string sql = @"UPDATE [dbo].[Mailing]
                             SET [Nome] = @Nome
                                ,[Email] = @Email
                                ,[Telefone] = @Telefone
                                ,[Logradouro] = @Logradouro
                                ,[Estado] = @Estado
                                ,[Cidade] = @Cidade
                                ,[Genero] = @Genero
                           WHERE id = @Id ";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = mailing.Id,
                Nome = mailing.Nome,
                Email = mailing.Email,
                Telefone = mailing.Telefone,
                Logradouro = mailing.Logradouro,
                Estado = mailing.Estado,
                Cidade = mailing.Cidade,
                Genero = mailing.Genero
            }, _dbConnector.dbTransaction);
        }

        // DELETAR MAILING
        public async Task DeletarMailing(string mailingId)
        {
            string sql = $"DELETE FROM [dbo].[Mailing] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = mailingId }, _dbConnector.dbTransaction);
        }

        // VERIFICAR EXISTÊNCIA DE MAILING
        public async Task<bool> ExisteMailingPorID(string mailingId)
        {
            string sql = $"SELECT 1 FROM Mailing WHERE Id = @Id ";

            var users = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = mailingId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
      
        // RETORNAR MAILING POR ID
        public async Task<MailingModel> RetornarMailingID(string mailingId)
        {
            string sql = $"{baseSql} AND Id = @Id";

            var users = await _dbConnector.dbConnection.QueryAsync<MailingModel>(sql, new { Id = mailingId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }

        // LISTAR MAILING POR FILTRO
        public async Task<List<MailingModel>> ListarMailingPorFiltro(string mailingId = null, string nome = null)
        {
            string sql = $"{baseSql} ";

            if (!string.IsNullOrWhiteSpace(mailingId))
                sql += "AND Id = @Id";

            if (!string.IsNullOrWhiteSpace(nome))
                sql += "AND Nome like @Nome";

            var users = await _dbConnector.dbConnection.QueryAsync<MailingModel>(sql, new { Id = mailingId, Nome = "%" + nome + "%" }, _dbConnector.dbTransaction);

            return users.ToList();
        }
    }
}
