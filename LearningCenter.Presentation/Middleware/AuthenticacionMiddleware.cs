using LearningCenter.Domain.Security.Repositories;
using LearningCenter.Domain.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LearningCenter.Presentation.Middleware;

public class AuthenticacionMiddleware
{
    private readonly RequestDelegate _next;
 

    public AuthenticacionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context,ITokenService tokenService,IUserRepository userRepository)
    {
        //attrubute allow anonymus
        var allowAnonymous = await IsAllowAnonymousAsync(context);

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }
        
        //If token exists
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is missing");
            return;
        }
        //validate token
        var userId = await tokenService.ValidateToken(token);

        if (userId == null || userId == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is corrupted");
            return;
        }

        var user = await userRepository.GetUserById(userId.Value);
        context.Items["User"] = user;
        
        await _next(context);
    }
    
    private Task<bool> IsAllowAnonymousAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null) return Task.FromResult(false);

        var allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null;

        if (!allowAnonymous)
        {
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor != null)
                allowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
        }

        return Task.FromResult(allowAnonymous);
    }
    

}