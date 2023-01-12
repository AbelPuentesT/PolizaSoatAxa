using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Exceptions;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PolizaSOAT.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;
        public TokenService(IConfiguration configuration, ISecurityService securityService, IPasswordService passwordService)
        {
            _configuration = configuration;
            _securityService = securityService;
            _passwordService = passwordService;
        }
        public async Task<(bool, Security)> IsValidUser(UserLogin login)
        {
            var user = await _securityService.GetLoginByCredentials(login);
            if (user == null)
            {
                throw new BusinessException("Usuario no existe");
            }
            var isValid = _passwordService.Check(user.Password, login.Password);
            return (isValid, user);
        }

        public string GenerateToken(Security security)
        {
            //Header 
            var _SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.User),
                new Claim("UserName", security.UserName),
                new Claim(ClaimTypes.Role,security.Rol.ToString())
            };
            //Payload
            var payLoad = new JwtPayload
                (
                _configuration["Authentication:Isser"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(Double.Parse(_configuration["TimeToken:DefaultTimeToken"]))
                ); ;
            var token = new JwtSecurityToken(header, payLoad);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
