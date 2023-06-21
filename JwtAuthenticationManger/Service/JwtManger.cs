using JwtAuthenticationManger.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManger.Service
{
    public class JwtManger
    {
        public const string jwt_Key = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";

        private readonly List<AppUser> _appUser = new()
        {
              new AppUser {UserName ="Admin", Role="Admin", Password="Admin123&"},
              new AppUser {UserName ="User", Role="User", Password="Admin123&"}
        };

        public AuthenticationReponse Authenticate (LoginModel user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
                return null;

            //validate
            var userAccount = _appUser.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            if (userAccount == null)
                return null;
            var tokenhandle = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwt_Key);
            var tokenexpiretime = DateTime.Now.AddMinutes(10);
            var claimidentity = new ClaimsIdentity(new List<Claim>
            {
               
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                 new Claim("Role", userAccount.Role)

            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimidentity,
                Expires = tokenexpiretime,
                SigningCredentials = signingCredentials
            };
            var token = tokenhandle.CreateToken(tokenDescriptor);
            var jwttoken = tokenhandle.WriteToken(token);
            return new AuthenticationReponse
            {
                AccessToken = jwttoken,
                ExpiresIn = (int)tokenexpiretime.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
