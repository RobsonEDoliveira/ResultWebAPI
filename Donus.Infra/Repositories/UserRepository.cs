using Dapper;
using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Repositories.DataConnector;
using Donus.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnector _dbConnector;
        public UsuarioRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        const string baseSql = @"SELECT [Id]
                                       ,[Nome]
                                       ,[Login]
                                       ,[PasswordHash]
                                       ,[DataCadastro]
                                   FROM [dbo].[Usuario]
                                   WHERE 1 = 1 ";

        // CRIAR NOVO USUÁRIO
        public async Task CriarUsuario(UsuarioModel user)
        {
            string sql = @"INSERT INTO [dbo].[Usuario]
                                      ([Id]
                                      ,[Nome]
                                      ,[Login]
                                      ,[PasswordHash]
                                      ,[DataCadastro])
                                  VALUES
                                       (@Id
                                       ,@Nome
                                       ,@Login
                                       ,@PasswordHash
                                       ,@DataCadastro)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = user.Id,
                Nome = user.Nome,
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                DataCadastro = user.DataCadastro
            }, _dbConnector.dbTransaction);
        }

        // ATUALIZAR USUÁRIO
        public async Task AtualizarUsuario(UsuarioModel user)
        {
            string sql = @"UPDATE [dbo].[Usuario]
                             SET [Nome] = @Nome
                                ,[Login] = @Login
                                ,[PasswordHash] = @PasswordHash
                           WHERE id = @Id ";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = user.Id,
                Name = user.Nome,
                Login = user.Login,
                PasswordHash = user.PasswordHash
            }, _dbConnector.dbTransaction);
        }

        // DELETAR USUÁRIO
        public async Task DeletarUsuario(string userId)
        {
            string sql = $"DELETE FROM [dbo].[Usuario] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = userId }, _dbConnector.dbTransaction);
        }

        // VERIFICAR EXISTÊNCIA DE USUÁRIO
        public async Task<bool> VerificarUsuarioEXISTENTE(string userId)
        {
            string sql = $"SELECT 1 FROM Usuario WHERE Id = @Id ";

            var users = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = userId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }

        // VERIFICAR EXISTÊNCIA DE LOGIN
        public async Task<bool> VerificarLoginEXISTENTE(string login)
        {
            string sql = $"SELECT 1 FROM [Usuario] WHERE Login = @Login ";

            var users = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Login = login }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }

        // RETORNAR USUÁRIO POR ID
        public async Task<UsuarioModel> RetornarUsuarioID(string userId)
        {
            string sql = $"{baseSql} AND Id = @Id";

            var users = await _dbConnector.dbConnection.QueryAsync<UsuarioModel>(sql, new { Id = userId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }

        // RETORNAR USUÁRIO POR FILTRO
        public async Task<List<UsuarioModel>> ListarUsuarioPorFILTRO(string login = null, string nome = null)
        {
            string sql = $"{baseSql} ";

            if (!string.IsNullOrWhiteSpace(login))
                sql += "AND login = @Login";

            if (!string.IsNullOrWhiteSpace(nome))
                sql += "AND Nome like @Nome";

            var users = await _dbConnector.dbConnection.QueryAsync<UsuarioModel>(sql, new { Login = login, Nome = "%" + nome + "%" }, _dbConnector.dbTransaction);

            return users.ToList();
        }

        // RETORNAR LOGIN
        public async Task<UsuarioModel> RetornarUsuarioLOGIN(string login)
        {
            string sql = $"{baseSql} AND Login = @Login";

            var users = await _dbConnector.dbConnection.QueryAsync<UsuarioModel>(sql, new { Login = login }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
    }
}
