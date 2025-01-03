namespace WebApplication.MVC.Controllers.src;

public class HttpPostAttribute(string path) : HttpMethodAttribute("POST", path);