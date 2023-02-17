using Donus.Domain.Common;
using Donus.Domain.Interfaces.Repositories;
using Donus.Domain.Interfaces.Services;
using Donus.Domain.Models;
using Donus.Domain.Validations;
using Donus.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Donus.Domain.Services
{
    // USUÁRIO SERVICE
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly IGenerators _generators;
        private readonly ISecurityService _securityService;

        public UsuarioService(IUsuarioRepository UsuarioRepository,
                           ITimeProvider timeProvider,
                           IGenerators generators, 
                           ISecurityService securityService)
        {
            _UsuarioRepository = UsuarioRepository;
            _timeProvider = timeProvider;
            _generators = generators;
            _securityService = securityService;
        }

        // AUTENTICAR
        public async Task<Response<bool>> AutenticarUsuario(string password, UsuarioModel user)
        {
            return await _securityService.ValidarPassword(password, user);
        }

        // CRIAR
        public async Task<Response> CriarUsuario(UsuarioModel user)
        {
            var response = new Response();

            var validation = new UserValidation();
            var errors = validation.Validate(user).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            user.Id = _generators.Generate();
            user.DataCadastro = _timeProvider.utcDateTime();

            await _UsuarioRepository.CriarUsuario(user);

            return response;
        }

        // DELETAR
        public async Task<Response> DeletarUsuario(string userId)
        {
            var response = new Response();

            var exists = await _UsuarioRepository.VerificarUsuarioEXISTENTE(userId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {userId} not exists!"));
                return response;
            }

            await _UsuarioRepository.DeletarUsuario(userId);

            return response;
        }

        // RETORNAR POR ID
        public async Task<Response<UsuarioModel>> RetornarUsuarioID(string userId)
        {
            var response = new Response<UsuarioModel>();

            var exists = await _UsuarioRepository.VerificarUsuarioEXISTENTE(userId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {userId} not exists!"));
                return response;
            }

            var data = await _UsuarioRepository.RetornarUsuarioID(userId);
            response.Data = data;
            return response;
        }

        // RETORNAR LOGIN
        public async Task<Response<UsuarioModel>> RetornarUsuarioLOGIN(string login)
        {
            var response = new Response<UsuarioModel>();

            var exists = await _UsuarioRepository.VerificarLoginEXISTENTE(login);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {login} not exists!"));
                return response;
            }

            var data = await _UsuarioRepository.RetornarUsuarioLOGIN(login);
            response.Data = data;
            return response;
        }

        // LISTAR POR FILTRO
        public async Task<Response<List<UsuarioModel>>> ListarUsuarioPorFILTRO(string userId = null, string name = null)
        {
            var response = new Response<List<UsuarioModel>>();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var exists = await _UsuarioRepository.VerificarUsuarioEXISTENTE(userId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"User {userId} not exists!"));
                    return response;
                }
            }

            var data = await _UsuarioRepository.ListarUsuarioPorFILTRO(userId, name);
            response.Data = data;

            return response;
        }

        // ATUALIZAR USUÁRIO
        public async Task<Response> AtualizarUsuario(UsuarioModel user)
        {
            var response = new Response();

            var validation = new UserValidation();
            var errors = validation.Validate(user).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _UsuarioRepository.VerificarUsuarioEXISTENTE(user.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {user.Id} not exists!"));
                return response;
            }

            await _UsuarioRepository.AtualizarUsuario(user);

            return response;
        }
    }
}
