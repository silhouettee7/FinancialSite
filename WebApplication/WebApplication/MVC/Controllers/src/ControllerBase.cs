using System.Net;
using System.Text.Json;

namespace WebApplication.MVC.Controllers.src;

public abstract class ControllerBase
{
    protected HttpListenerContext Context { get; private set; }

    public void SetContext(HttpListenerContext context)
    {
        Context = context;
    }

    protected async Task<T> GetResponseData<T>()
    {
        using var reader = new StreamReader(Context.Request.InputStream);
        var body = await reader.ReadToEndAsync();
        var dto = JsonSerializer.Deserialize<T>(body);
        return dto;
    }
    
    protected void Ok()
    {
        Context.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}