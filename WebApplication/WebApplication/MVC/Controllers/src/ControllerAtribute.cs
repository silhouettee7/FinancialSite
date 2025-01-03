namespace WebApplication.MVC.Controllers.src;

[AttributeUsage(AttributeTargets.Class)]
public class ControllerAttribute(string route) : Attribute
{
    public string Route { get; } = route;
}