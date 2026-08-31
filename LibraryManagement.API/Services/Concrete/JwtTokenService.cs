using LibraryManagement.API.Services.Abstract;
using LibraryManagement.DataAccess.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryManagement.API.Services.Concrete
{
    public class JwtTokenService : IJwtTokenService //komple bak

    {

        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(UserLoginResultDto userLoginResultDto) //claim ne
        {
            var claims = new List<Claim> {

                new Claim(ClaimTypes.NameIdentifier, userLoginResultDto.Id.ToString()), //niye to string niye new
                new Claim (ClaimTypes.Email, userLoginResultDto.Email),
                new Claim (ClaimTypes.Role, userLoginResultDto.Role.ToString())
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not found")));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience:  _configuration["Jwt:Audience"], //cibstrc oldugu icin iki nokta estttir deglil
                claims : claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );  // bazilarlinda {} bazilarinda () niye

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);


        }
    }
}
