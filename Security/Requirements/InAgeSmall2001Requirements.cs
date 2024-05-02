using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_web_api.Security.Requirements
{
    public class InAgeSmall2001Requirements:IAuthorizationRequirement
    {
        public InAgeSmall2001Requirements(int min,int max)
        {
            this.MinYear=min;
            this.MaxYear=max;
        }
        public int MinYear{set;get;}
        public int MaxYear{get;set;}
    }
}