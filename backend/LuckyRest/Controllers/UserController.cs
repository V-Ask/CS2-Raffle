using LuckyRest.Database.Entities;
using LuckyRest.Services;
using LuckyRest.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    [HttpGet("auth")]
    public IActionResult CheckAuth()
    {
        return Ok();
    }
}