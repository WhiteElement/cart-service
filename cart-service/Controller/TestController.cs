using Microsoft.AspNetCore.Mvc;

namespace cart_service.Controller;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Test()
    {
        return "Running . . .";
    }
}