using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Token
{
    public class TokenModel
    {
        public string RefreshToken{set;get;}=string.Empty;
        public string AcccessToken{set;get;}=string.Empty;

    }
}