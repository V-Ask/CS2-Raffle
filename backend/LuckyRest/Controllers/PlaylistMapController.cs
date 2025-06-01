using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuckyRest.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlaylistMapController : ControllerBase
{
}