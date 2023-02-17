using AutoMapper;
using Donus.Application.DataContract.Request.Usuario;
using Donus.Application.DataContract.Response.Usuario;
using Donus.Application.Interfaces;
using Donus.Application.Interfaces.Security;
using Donus.Domain.Interfaces.Services;
using Donus.Domain.Models;
using Donus.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donus.Application.Applications
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly ITokenManager _tokenManager;

        public UsuarioApplication(IUsuarioService UsuarioService, IMapper mapper, ISecurityService securityService, ITokenManager tokenManager)
        {
            _UsuarioService = UsuarioService;
            _mapper = mapper;
            _securityService = securityService;
            _tokenManager = tokenManager;
        }

        // AUTENTICAR
        public async Task<Response<AuthResponse>> AuthAsync(AuthRequest auth)
        {
            var user = await _UsuarioService.RetornarUsuarioLOGIN(auth.Login);

            if (user.Report.Any())
                return Response.Unprocessable<AuthResponse>(user.Report);

            var isAuthenticated = await _UsuarioService.AutenticarUsuario(auth.Password, user.Data);

            if (!isAuthenticated.Data)
                return Response.Unprocessable<AuthResponse>(new List<Report>() { Report.Create("Invalid password or login") });

            var token = await _tokenManager.GenerateTokenAsync(user.Data);

            return new Response<AuthResponse>(token);
        }

        // CRIAR
        public async Task<Response> CriarUsuario(CreateUsuarioRequest User)
        {
            try
            {
                var isEquals = await _securityService.CompararPassword(User.Password, User.ConfirmPassword);

                if (!isEquals.Data)
                    return Response.Unprocessable(Report.Create("Passwords do not match"));

                var passwordEncripted = await _securityService.CryptografarPassword(User.Password);

                User.Password = passwordEncripted.Data;

                var UsuarioModel = _mapper.Map<UsuarioModel>(User);

                return await _UsuarioService.CriarUsuario(UsuarioModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

        // LISTAR
        public async Task<Response<List<UsuarioResponse>>> ListarUsuarioPorFILTRO(string userId = null, string name = null)
        {
            try
            {
                Response<List<UsuarioModel>> user = await _UsuarioService.ListarUsuarioPorFILTRO(userId, name);

                if (user.Report.Any())
                    return Response.Unprocessable<List<UsuarioResponse>>(user.Report);

                var response = _mapper.Map<List<UsuarioResponse>>(user.Data);

                return Response.OK(response);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable<List<UsuarioResponse>>(new List<Report>() { response });
            }
        }
    }
}
