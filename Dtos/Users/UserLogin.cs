using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Users
{
    public class UserLogin
    {
        [Required]
        public string Username{set;get;}=string.Empty;
        [Required]
        public string Password{set;get;}=string.Empty; 
    }
}