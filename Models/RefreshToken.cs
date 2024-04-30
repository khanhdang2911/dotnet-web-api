using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id{set;get;}
        public int UserId{set;get;}
        [ForeignKey("UserId")]
        public Users? Users{set;get;}
        public string Token{set;get;}=string.Empty;
    }
}