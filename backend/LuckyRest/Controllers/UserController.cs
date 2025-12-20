using LuckyRest.Database.DTOs.Models;
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
public class UserController(
    UserManager<User> userManager,
    IUserService userService) : ControllerBase
{
    [HttpGet("auth")]
    public async Task<ActionResult<AuthUserDto>> CheckAuth()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();
        var dto = userService.AuthUser(user);
        if (dto.Data == null) return Unauthorized();
        return dto.Data;
    }
}