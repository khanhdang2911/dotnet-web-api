using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Models
{
    public class UserRole
    {
        public int UsersID{set;get;}
        public int RoleID{set;get;}
        public Users Users{set;get;}=new Users();
        public Role Role{set;get;}=new Role();
    }
}