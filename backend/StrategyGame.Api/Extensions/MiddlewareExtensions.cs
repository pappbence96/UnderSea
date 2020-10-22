using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middlewares.ExceptionHandling.CustomExceptionHandlingMiddleware>();
            return app;
        }
    }
}
