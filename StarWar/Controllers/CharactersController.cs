using Microsoft.AspNetCore.Mvc;

namespace StarWar.Controllers;

[ApiController]
[Route("[controller]")]
public class CharactorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return Ok("Hello World!");
    }
}