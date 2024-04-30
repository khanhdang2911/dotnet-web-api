using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Fullname{set;get;}=string.Empty;
        [EmailAddress]
        public string Email{set;get;}=string.Empty;
        [MaxLength(20,ErrorMessage ="do dai toi da la 20 ki tu")]
        public string Username{set;get;}=string.Empty;
        public string Password{set;get;}=string.Empty; 
    }
}