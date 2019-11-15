namespace JWTNetCoreVue.Controllers
{
  using JWTNetCoreVue.Models;
  using JWTNetCoreVue.Entities;
  using JWTNetCoreVue.Services;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Classe de controlleur UsersController.
    /// Controlleur pour les <see cref="User">Utilisateurs</see>
    /// </summary>
    [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    /// <summary>
    /// Le service des utilisateurs.
    /// </summary>
    private readonly IUserService _userService;

    private readonly ILogger<UsersController> _logger;

    /// <summary>
    /// Instancie une nouvelle instance de <see cref="UsersController"/>.
    /// </summary>
    /// <param name="userService">Le <see cref="IUserService"/>.</param>
    public UsersController(IUserService userService,
      ILogger<UsersController> logger)
    {
      _userService = userService;
      _logger = logger;
      _logger.LogInformation("Index page says hello");

    }

    /// <summary>
    /// Authentifie un <see cref="User">Utilisateur</see>
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> utilisé pour authentifier <see cref="User">Utilisateur</see></param>
    /// <returns>L'<see cref="User">Utilisateur</see> si l'authentification est ok.</returns>
    [AllowAnonymous]
    [HttpPost("auth")]
    public IActionResult Authenticate([FromBody]UserAuthenticateModel model)
    {
      var user = _userService.Authenticate(model);

      if (user == null)
      {
        return BadRequest(new { message = "Username or password is incorrect" });
      }
      else
      {
        return Ok(user);
      }
    }

    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult Test()
    {
      _logger.LogCritical("Index page says hello");
      return Ok();
    }

    /// <summary>
    /// Obtient les informations de l'utilisateur en cours.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
      User user = _userService.GetCurrentUser();
      return Ok(user);
    }
  }
}
