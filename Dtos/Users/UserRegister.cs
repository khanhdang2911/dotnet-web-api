using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Users
{
    public class UserRegister
    {
         public string Fullname{set;get;}=string.Empty;
        [EmailAddress]
        public string Email{set;get;}=string.Empty;
        [MaxLength(20,ErrorMessage ="do dai toi da la 20 ki tu")]
        public string Username{set;get;}=string.Empty;
        public string Password{set;get;}=string.Empty; 
    }
}