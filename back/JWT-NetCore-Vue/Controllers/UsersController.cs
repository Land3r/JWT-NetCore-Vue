namespace JWTNetCoreVue.Controllers
{
  using System;
  using System.Globalization;
  using JWTNetCoreVue.Entities.Db;
  using JWTNetCoreVue.Entities.Emails;
  using JWTNetCoreVue.Entities.Users;
  using JWTNetCoreVue.Helpers;
  using JWTNetCoreVue.Models.Users;
  using JWTNetCoreVue.Services;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  /// <summary>
  /// Classe de controlleur UsersController.
  /// Controlleur pour les <see cref="User">Utilisateurs</see>.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings _appSettings;

    /// <summary>
    /// Le service des utilisateurs.
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    /// Le service de réinitialisation des mots de passes utilisateurs.
    /// </summary>
    private readonly IUserPasswordResetTokenService _userPasswordResetTokenService;

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
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="userService">Le <see cref="IUserService"/>.</param>
    /// <param name="userPasswordResetTokenService">Le <see cref="IUserPasswordResetTokenService"/>.</param>
    /// <param name="emailService">Le <see cref="IEmailService"/>.</param>
    /// <param name="logger">Le logger utilisé.</param>
    /// <param name="localizer">Les ressources localisées.</param>
    public UsersController(IOptions<AppSettings> appSettings,
      IUserService userService,
      IUserPasswordResetTokenService userPasswordResetTokenService,
      IEmailService emailService,
      ILogger<UsersController> logger,
      IStringLocalizer<UsersController> localizer)
    {
      _appSettings = appSettings.Value;
      _userService = userService;
      _userPasswordResetTokenService = userPasswordResetTokenService;
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
        _logger.LogWarning(string.Format(CultureInfo.InvariantCulture, _localizer["LogLoginFailed"].Value, model?.Username));
        return BadRequest(new { message = _localizer["LoginFailed"].Value });
      }
      else
      {
        _logger.LogInformation(string.Format(CultureInfo.InvariantCulture, _localizer["LogLoginSuccess"].Value, model?.Username));
        return Ok(user);
      }
    }

    /// <summary>
    /// Enregistre un nouvel utilisateur.
    /// </summary>
    /// <param name="model">Les données de l'utilisateur à créer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]User model)
    {
      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogRegisterTry"].Value));

      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      User user;
      try
      {
        user = _userService.Register(model);
      }
      catch (Exception ex)
      {
        if (ex.GetType() == typeof(ArgumentException))
        {
          _logger.LogWarning(string.Format(CultureInfo.InvariantCulture, ex.Message));
          return Conflict(new { message = ex.Message });
        }
        throw;
      }

      if (user == null)
      {
        _logger.LogWarning(string.Format(CultureInfo.InvariantCulture, _localizer["LogRegisterFailed"].Value, model?.Username));
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
    /// Génére un token de réinitialisation de mot de passe utilisateur.
    /// </summary>
    /// <param name="model">Les données de l'utilisateur dont le mot de passe doit être réinitialiser.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpPost("forgotpassword")]
    public IActionResult ForgotPassword([FromBody]UserPasswordLostModel model)
    {
      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordLostTokenTry"].Value));

      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      User user = null;
      if (!string.IsNullOrEmpty(model.Email))
      {
        user = _userService.GetByEmail(model.Email);
      }
      else if (!string.IsNullOrEmpty(model.Username))
      {
        user = _userService.GetByUsername(model.Username);
      }

      if (user == null)
      {
        _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordLostTokenUserNotFound"].Value, new { method = !string.IsNullOrEmpty(model.Email) ? "email" : "username", value = model.Email ?? model.Username }));
        return NotFound(new { message = string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordLostTokenUserNotFound"].Value) });
      }

      UserPasswordResetToken userPasswordResetToken;
      string token;
      try
      {
        token = CryptographicHelper.GetUrlSafeToken(24);
        userPasswordResetToken = new UserPasswordResetToken()
        {
          Token = token,
          Created = DateTime.UtcNow,
          CreatedBy = new UserReference() { Id = user.Id, Username = user.Username }
        };
        userPasswordResetToken = _userPasswordResetTokenService.Create(userPasswordResetToken);

        // Envoie de l'email, avec le token en clair.
        _emailService.SendTemplate(new EmailAddress() { Address = user.Email, Name = user.Username }, "PasswordLost", new
        {
          username = user.Username,
          resetpasswordlink = $"{new Uri(new Uri(_appSettings.Environment.FrontUrl), $"#/resetpassword/{token}")}",
          sitename = _appSettings.Environment.Name,
          siteurl = _appSettings.Environment.FrontUrl
        });
      }
      catch (Exception ex)
      {
        // TODO: Gérer les exceptions, avec message localisé
        _logger.LogError(string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordLostTokenFailed"].Value));
        throw ex;
      }

      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordLostTokenSuccess"].Value, new { value = model.Email ?? model.Username }));
      return Ok();
    }

    /// <summary>
    /// Vérifie si le token fourni existe.
    /// </summary>
    /// <param name="model">Le token  valider.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpGet("passwordreset/{token}")]
    public IActionResult PasswordResetExists(string token)
    {
      _logger.LogDebug(string.Format(CultureInfo.InvariantCulture, _localizer["LogPasswordResetExistsTry"].Value));

      if (string.IsNullOrEmpty(token))
      {
        throw new ArgumentNullException(nameof(token));
      }

      UserPasswordResetToken userPasswordResetToken;
      try
      {
        userPasswordResetToken = _userPasswordResetTokenService.GetByToken(token);
      }
      catch (Exception ex)
      {

        throw ex;
      }


      return Ok();
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
