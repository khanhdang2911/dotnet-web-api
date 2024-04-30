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
    }
}