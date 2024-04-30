using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Token;
using dotnet_web_api.Models;

namespace dotnet_web_api.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(Users users);
        public string CreateRefreshToken();
        public string RenewToken(TokenModel model);
    }
}