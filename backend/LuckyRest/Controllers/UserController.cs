using LuckyRest.Database.Entities;
using LuckyRest.Services;
using LuckyRest.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService, UserManager<User> userManager) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> SetLaunchServerEndpoint(string endpoint)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        await userService.SetUserRestEndpoint(user, endpoint);
        return Ok();
    }
}