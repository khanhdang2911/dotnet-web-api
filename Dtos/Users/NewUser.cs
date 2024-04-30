using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Users
{
    public class NewUser
    {
        public string Email{set;get;}=string.Empty;
        public string Username{set;get;}=string.Empty;
        public string Token{set;get;}=string.Empty;
    }
}