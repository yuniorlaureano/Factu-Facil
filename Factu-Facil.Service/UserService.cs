using FactuFacil.Entity;
using FactuFacil.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Authentication;

namespace FactuFacil.Service
{
    public interface IUserService : IBaseService<User>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }

    public class UserService : BaseService<User>, IUserService
    {
        private readonly AppSettings _appSettings;
        public UserService(IUserRepository repository, IOptions<AppSettings> options) : base(repository)
        {
            _appSettings = options.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            User user = await _repository.GetOne(p => p.Email == request.Email);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new AuthenticationException("Usuario o contraseña inválido");
            }

            var token = generateToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string generateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
