using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName{set;get;}=string.Empty;
        public List<UserRole> userRoles{set;get;}=new List<UserRole>();

    }
}