using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Token;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_web_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config,ApplicationDBContext context)
        {
            _context = context;
            _config=config;
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }


        public string CreateToken(Users users)
        {
            var claims=new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,users.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,users.Username),
                new Claim("Id",users.Id.ToString())
            };
            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor()
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddSeconds(20),
                SigningCredentials=creds,
                Issuer=_config["JWT:Issuer"],
                Audience=_config["JWT:Audience"]
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string CreateRefreshToken()
        {
            var random=new byte[32];
            using(var ran=RandomNumberGenerator.Create())
            {
                ran.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public string RenewToken(TokenModel model)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var tokenParam=new TokenValidationParameters()
            {
                ValidateIssuer=true,
                ValidIssuer=_config["JWT:Issuer"],
                ValidateAudience=true,
                ValidAudience=_config["JWT:Audience"],
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])
                ),
                ValidateLifetime=false
            };

            try{
                // Check format token
                var tokenInVerification=tokenHandler.ValidateToken(model.AcccessToken,tokenParam,out var validateToken);
                // Check algorithm for secret_key
                if(validateToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result=jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,StringComparison.InvariantCultureIgnoreCase);
                    if(result==false)
                    {
                        return "error";
                    }
                }
                // Check token has expried
                var utcExpireDate=long.Parse(tokenInVerification.Claims.First(x=>x.Type==JwtRegisteredClaimNames.Exp).Value);

                var expireDate=ConvertUnixTimeToDateTime(utcExpireDate);

                if(expireDate>DateTime.UtcNow)
                {
                    return "error";
                }
                // Check refresh token in DB
                if(_context.refreshTokens.Any(r=>r.Token==model.RefreshToken)==false)
                {
                    return "error";
                }

                var userId=_context.refreshTokens.Where(r=>r.Token==model.RefreshToken).Select(r=>r.UserId).First();
                var user=_context.users.Find(userId);

                return CreateToken(user);

            }
            catch{
                return "error";
            }

        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval=new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);

            dateTimeInterval.AddSeconds(utcExpireDate);
            return dateTimeInterval;
        }
    }
}