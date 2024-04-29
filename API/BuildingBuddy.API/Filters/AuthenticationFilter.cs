using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BuildingBuddy.API.Filters;


public class BearerTokenAuthorizationFilter : AuthorizeAttribute
{
    private string[] _roles;

    public BearerTokenAuthorizationFilter(params string[] roles)
    {
        _roles = roles;
    }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        // Verificar si se proporciona un token Bearer en el encabezado de autorización de la solicitud HTTP
        var authHeader = actionContext.Request.Headers.Authorization;
        if (authHeader == null || authHeader.Scheme != "Bearer")
        {
            return false; // No se proporcionó un token Bearer
        }

        // Obtener el token Bearer de la solicitud
        var token = authHeader.Parameter;

        // Decodificar y validar el token Bearer
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        // Verificar la validez del token y obtener los claims (incluidos los roles)
        var roles = tokenS.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        // Verificar si el usuario tiene alguno de los roles requeridos
        foreach (var role in _roles)
        {
            if (roles.Contains(role))
            {
                return true; // El usuario tiene al menos uno de los roles requeridos
            }
        }

        // Si el usuario no tiene ninguno de los roles requeridos, devolver false
        return false;
    }
}