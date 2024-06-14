using Cooklee.Data.Entities.Identity;
using Cooklee.Data.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cooklee.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreatTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            // Private Claims (User-Defined)
            var authClaims = new List<Claim>()
            {
                new Claim ("Email", user.Email),
                new Claim ("UserId", user.Id),
            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }



            // Secret Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            // Generate + Validate Token (Registered Claim)
            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudiance"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(2),
                claims: authClaims,
                // Secret Key + Algorithm
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
