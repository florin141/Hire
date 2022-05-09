using Hire.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace Hire.WebApi.Filters
{
    public class JsonExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;

        public JsonExceptionsFilter(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_environment.IsDevelopment())
            {
                error.Detail = context.Exception.StackTrace;
                error.Message = context.Exception.Message;
            }
            else
            {
                error.Detail = context.Exception.Message;
                error.Message = "A server error occurred.";
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
