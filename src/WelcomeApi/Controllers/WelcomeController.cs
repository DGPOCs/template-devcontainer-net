using Microsoft.AspNetCore.Mvc;
using WelcomeApi.Models;
using WelcomeApi.Services;

namespace WelcomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WelcomeController : ControllerBase
{
    private readonly WelcomeService _welcomeService;

    public WelcomeController(WelcomeService welcomeService)
    {
        _welcomeService = welcomeService;
    }

    [HttpGet]
    public ActionResult<WelcomeMessageResponse> Get()
    {
        var message = _welcomeService.GetWelcomeMessage();
        return Ok(new WelcomeMessageResponse(message));
    }

    [HttpPost]
    public ActionResult<WelcomeMessageResponse> Post([FromBody] WelcomeMessageRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updatedMessage = _welcomeService.UpdateWelcomeMessage(request.Message);
        return Ok(new WelcomeMessageResponse(updatedMessage));
    }
}
