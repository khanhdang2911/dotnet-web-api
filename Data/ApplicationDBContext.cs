using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Data
{
    public class ApplicationDBContext:DbContext
    {
            public ApplicationDBContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<Stock> stocks{set; get; }
            public DbSet<Comment> comments{set; get; }

            public DbSet<Users> users{set; get; }
            public DbSet<RefreshToken> refreshTokens{set;get;}
            public DbSet<Role> roles{set;get;}
            public DbSet<UserRole> userRoles{set;get;}
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<UserRole>(options=>{
                    options.HasKey(u=>new {u.UsersID, u.RoleID});
                });
            }
    }
}