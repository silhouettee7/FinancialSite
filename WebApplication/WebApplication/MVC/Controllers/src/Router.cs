using System.Net;
using System.Reflection;

namespace WebApplication.MVC.Controllers.src;

public class Router
{
    private readonly Dictionary<string,Type> _controllers = new ();

    public void RegisterControllers(Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ControllerAttribute>() != null);
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<ControllerAttribute>();
            if (attribute is null) return;
            _controllers[attribute.Route] = type;
        }
    }

    public void HandleRequest(HttpListenerContext context)
    {
        var request = context.Request;
        var path = request.Url.AbsolutePath.Trim('/');
        var httpMethod = request.HttpMethod;
        foreach (var (route, controllerType) in _controllers)
        {
            if (path.StartsWith(route))
            {
                var controller = Activator.CreateInstance(controllerType) as ControllerBase;
                if (controller == null) break;
                controller.SetContext(context);

                var methods = controllerType.GetMethods()
                    .Where(m => m.GetCustomAttribute<HttpMethodAttribute>() != null);
                foreach (var method in methods)
                {
                    var attr = method.GetCustomAttribute<HttpMethodAttribute>();
                    if (attr != null 
                        && httpMethod.Equals(attr.Method, StringComparison.OrdinalIgnoreCase) 
                        && path.Equals($"{route.Trim('/')}/{attr.Path.Trim('/')}", StringComparison.OrdinalIgnoreCase))
                    {
                        method.Invoke(controller, null);
                        return;
                    }
                }
            }
        }
        context.Response.StatusCode = 404;
    }
}