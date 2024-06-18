using LearningCenter.Domain.Security.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningCenter.Presentation.Security.Filters;

public class AuthorizeCustomAttribute : Attribute,IAsyncAuthorizationFilter
{
    private readonly string[] _roles;
    
    public AuthorizeCustomAttribute(params string[] roles)
    {
        _roles = roles;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
      var user = context.HttpContext.Items["User"] as User;

      if (user == null || !_roles.Any(role => user.Role.Contains(role)))
      {
          context.Result = new UnauthorizedResult();
      }
    }
}