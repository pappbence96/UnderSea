using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StrategyGame.Api.Middlewares.ExceptionHandling
{
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public CustomExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var error = new ErrorDetails();
            if (exception is ArgumentException || exception is InvalidOperationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                error.Message = exception.Message;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error.Message = "Internal server error.";
            }

            error.StatusCode = context.Response.StatusCode;
            return context.Response.WriteAsync(error.ToString());
        }
    }
}
