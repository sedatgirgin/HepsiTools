using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Abstract;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HepsiTools.MiddleWare
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandling> _logger;
        private readonly IErrorRepository _errorRepository;
        public ErrorHandling(RequestDelegate next, ILogger<ErrorHandling> logger, IErrorRepository errorRepository)
        {
            _next = next;
            _logger = logger;
            _errorRepository = errorRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandling> logger)
        {
            object errors = null;
            string source = null;

            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex, "Rest Error");
                    errors = re.Message;
                    source = "Rest Exception";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    break;

                case Exception e:
                    logger.LogError(ex, "Server Error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    source = "Server Error";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errors != null)
            {
                var strErrors = JsonConvert.SerializeObject(new
                {
                    errors
                });

               await _errorRepository.InsertAsync(new ErrorLog { Message = strErrors, Source = source, StackTrace = ex.StackTrace });

                await context.Response.WriteAsync(strErrors);
            }
        }
    }
}
