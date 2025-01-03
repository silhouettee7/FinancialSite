namespace WebApplication.MVC.Controllers.src;

public class HttpGetAttribute(string path) : HttpMethodAttribute("GET", path);