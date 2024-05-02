using dotnet_web_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_web_api.Security.Requirements
{
    public class AppAuthorizationHandler : IAuthorizationHandler
    {
        
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirment=context.PendingRequirements;
            foreach(var requirment in pendingRequirment)
            {
                if(requirment is InAgeSmall2001Requirements)
                {
                    if(CheckAge(context.Resource,(InAgeSmall2001Requirements) requirment))
                    {
                        context.Succeed(requirment);
                    }
                }
            }
            return Task.CompletedTask;
        }
        public bool CheckAge(object resource, InAgeSmall2001Requirements requirements)
        {
            var user=resource as Users;
            int userYear=user.BirthDay.Year;
            if(userYear>requirements.MaxYear||userYear<requirements.MinYear)
            {
                return false;
            }
            return true;
            
        }
    }
}