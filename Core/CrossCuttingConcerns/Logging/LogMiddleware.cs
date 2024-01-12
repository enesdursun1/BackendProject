using Azure.Core;
using Castle.Core.Internal;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging.LoggingModels;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.CrossCuttingConcerns.Logging;

public class LogMiddleware
{

    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public LogMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task InvokeAsync(HttpContext context)
    {
       
       
        var isLoggable = context.GetType().GetInterfaces().Any(i => i == typeof(ILoggable));

        //if (_next.GetMethodInfo().GetCustomAttribute<LoggableAttribute>() != null)
        //{

          






            var originalRequestBody = context.Request.Body;
        var originalResponseBody = context.Response.Body;
        string requestBodyText;
        string responseBodyText;

            using (var requestBodyStream = new MemoryStream())
            {
     
                await context.Request.Body.CopyToAsync(requestBodyStream);
         
            requestBodyStream.Seek(0, SeekOrigin.Begin);

               
                requestBodyText = await new StreamReader(requestBodyStream, Encoding.UTF8).ReadToEndAsync();

            List<LogParameter> logParameters = new()
            {

                new LogParameter { Type = context.GetType().Name, Value = requestBodyText }
            };


            LogDetail logDetail = new()
            {
                MethodName = _next.Method.Name,
                Parameters = logParameters,
                User = _httpContextAccessor.HttpContext == null ||
                   _httpContextAccessor.HttpContext.User.Identity.Name == null
                       ? "?"
                       : _httpContextAccessor.HttpContext.User.Identity.Name
            };

            _loggerServiceBase.Info(LogHelper.SerializeLogDetail(logDetail));


            requestBodyStream.Seek(0, SeekOrigin.Begin);

       
            context.Request.Body = requestBodyStream;


            await _next(context);

        }
       

         context.Request.Body = originalRequestBody;








        //}







    }

}
