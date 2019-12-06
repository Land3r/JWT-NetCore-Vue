namespace JWTNetCoreVue.Controllers
{
  using System.Globalization;
  using JWTNetCoreVue.Models;
  using JWTNetCoreVue.Entities;
  using JWTNetCoreVue.Services;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Localization;
  using System;
    using JWTNetCoreVue.Entities.Emails;

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

    /// <summary>
    /// Le service email.
    /// </summary>
    private readonly IEmailService _emailService;

    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    private readonly ILogger<UsersController> _logger;

    /// <summary>
    /// Les ressources de langue.
    /// </summary>
    private readonly IStringLocalizer<UsersController> _localizer;

    /// <summary>
    /// Instancie une nouvelle instance de <see cref="UsersController"/>.
    /// </summary>
    /// <param name="userService">Le <see cref="IUserService"/>.</param>
    public UsersController(IUserService userService,
      IEmailService emailService,
      ILogger<UsersController> logger,
      IStringLocalizer<UsersController> localizer)
    {
      _userService = userService;
      _emailService = emailService;
      _logger = logger;
      _localizer = localizer;
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
      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogLoginTry"].Value, model?.Username));
      var user = _userService.Authenticate(model);

      if (user == null)
      {
        _logger.LogInformation(string.Format(CultureInfo.InvariantCulture, _localizer["LogLoginFailed"].Value, model?.Username));
        return BadRequest(new { message = _localizer["LoginFailed"].Value });
      }
      else
      {
        _logger.LogInformation(string.Format(CultureInfo.InvariantCulture, _localizer["LogLoginSuccess"].Value, model?.Username));
        return Ok(user);
      }
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]User model)
    {
      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogRegisterTry"].Value));

      User user;
      try
      {
        user = _userService.Register(model);
      }
      catch (Exception ex)
      {
        if (ex.GetType() == typeof(ArgumentException))
        {
          return Conflict(new { message = ex.Message });
        }
        throw;
      }

      if (user == null)
      {
        _logger.LogInformation(string.Format(CultureInfo.InvariantCulture, _localizer["LogRegisterFailed"].Value, model?.Username));
        return BadRequest(new { message = _localizer["RegisterFailed"].Value });
      }
      else
      {
        _logger.LogInformation(string.Format(CultureInfo.InvariantCulture, _localizer["LogRegisterSuccess"].Value, model?.Username));

        // TODO: Creating email.
        EmailAddress to = new EmailAddress() { Name = model.Username, Address = model.Email };
        _emailService.TrySend(to, "Test Subject", "<h1>Coucou</h1><br /><p>Ceci est un paragraphe.</p>");
        return Ok(user);
      }
    }

    /// <summary>
    /// Obtient les informations de l'utilisateur en cours.
    /// </summary>
    /// <returns>L'<see cref="User">Utilisateur</see> en cours.</returns>
    [HttpGet]
    public IActionResult Get()
    {

      User user = _userService.GetByUsername("test");
      return Ok(user);
    }
  }
}
