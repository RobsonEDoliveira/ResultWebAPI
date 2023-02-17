using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Donus.Application.DataContract.Response.Usuario;
using Donus.Application.Interfaces.Security;
using Donus.Application.Models;
using Donus.Domain.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Donus.Application.Security
{
    public class TokenManager : ITokenManager
    {
        private readonly AuthSettings _authSettings;

        public TokenManager(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }

        // GERAR TOKEN
        public Task<AuthResponse> GenerateTokenAsync(UsuarioModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var date = DateTime.UtcNow;
            var expire = date.AddHours(_authSettings.ExpireIn);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = "DonusWebAPI",
                IssuedAt = date,
                NotBefore = date,
                Expires = expire,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new GenericIdentity(user.Login, JwtBearerDefaults.AuthenticationScheme), new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                })
            });

            var response = new AuthResponse()
            {
                Token = tokenHandler.WriteToken(securityToken),
                ExpireIn = _authSettings.ExpireIn,
                Type = JwtBearerDefaults.AuthenticationScheme
            };

            return Task.FromResult(response);
        }
    }
}
