using Brimo.IDP.STS.Identity.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandelExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandelExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            List<Error> errors = new List<Error>();
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string message = "";

            // check the type of exception we have 3 types
            // 1- Exceptions of type BaseException this is the exceptions that we throw in our application
            // 2- Exceptions of type BaseValidationException this exception thrown by the fluentvalidation
            // 3- Unhandeled Exceptions
            switch (ex)
            {
                case BusinessException re:
                    errors.Add(new Error(re.ErrorCode, re.Message));
                    context.Response.StatusCode = (int)re.StatusCode;
                    code = re.StatusCode;
                    message = "Brimo error";
                    //logger.BeginScope(ex, "Brimo ERROR", code);

                    _logger.LogError(re, "TypeOfException", "BusinessException");
                    break;
                case Exception e:
                    errors.Add(new Error("Server_Error", string.IsNullOrWhiteSpace(e.Message) ? "ERROR" : e.Message));
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Internal Server Error";
                    _logger.LogError(e, "TypeOfException", "ApplicationException");
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors.Count != 0)
            {
                var error = new ErrorMessage(errors, message, code);
                var results = JsonConvert.SerializeObject(error);
                await context.Response.WriteAsync(results);
            }
        }
    }

    public class ErrorMessage
    {
        public ErrorMessage(List<Error> errors, string message, HttpStatusCode code)
        {
            Errors = errors;
            Message = message;
            StatusCode = code;
        }
        public List<Error> Errors { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
    public class Error
    {
        public Error(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
