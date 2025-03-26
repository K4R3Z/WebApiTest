using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApiTest.Filters
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var allowAnonymous = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any();

            if (!allowAnonymous)
            {
                if (operation.Parameters.Any(p => p.Name == "Authorization"))
                    return;

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString("Basic ") }
                });
            }
        }
    }
}