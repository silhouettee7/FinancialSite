namespace WebApplication.MVC.Controllers.src;

[AttributeUsage(AttributeTargets.Method)]
public class HttpMethodAttribute(string method, string path): Attribute
{
    public string Method { get; } = method;
    public string Path { get; } = path;
}