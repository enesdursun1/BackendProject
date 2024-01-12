using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public class HttpExceptionHandler
{
    private HttpResponse? _response;
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }
    public Task HandleExceptionAsync(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = JsonSerializer.Serialize(exception.Message);
        return Response.WriteAsync(details);
    }


}
