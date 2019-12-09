namespace JWTNetCoreVue.Services
{
  using System;
  using System.Globalization;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using JWTNetCoreVue.Entities.Users;
  using JWTNetCoreVue.Extensions;
    using JWTNetCoreVue.Helpers;
    using JWTNetCoreVue.Models.Users;
  using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;
  using MongoDB.Driver;

  /// <summary>
  /// Classe UserService.
  /// Service pour la gestion des utilisateurs.
  /// </summary>
  public class UserService : AMongoEntityLocalizedService<User, UserService>, IUserService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string _collectionName = "Users";

    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings _appSettings;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="UserService"/>.
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public UserService(
      [FromServices]IStringLocalizer<UserService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<UserService> logger) : base(appSettings, _collectionName, logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        _appSettings = appSettings.Value;
      }
    }

    /// <summary>
    /// Authentifie un <see cref="User"/>, basé sur le <see cref="UserAuthenticateModel"/> fourni.
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> authentifié.</returns>
    public User Authenticate(UserAuthenticateModel model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      string hashedPassword = CryptographicHelper.GetHash(model.Password, _appSettings.Security.HashSalt);
      User user = Entities.Find(x => x.Username == model.Username && x.Password == hashedPassword).FirstOrDefault();

      if (user == null)
      {
        return null;
      }
      else
      {
        // Generation du token JWT.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Security.JWT.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString(CultureInfo.InvariantCulture))
          }),
          Expires = DateTime.UtcNow.AddDays(_appSettings.Security.JWT.DurationInDays),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);

        return user.WithoutPassword();
      }
    }

    /// <summary>
    /// Obtient un <see cref="User"/>, basé sur le username fourni.
    /// </summary>
    /// <param name="username">Le nom d'utilisateur à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see>.</returns>
    public User GetByUsername(string username)
    {
      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      return Entities.Find(elm => elm.Username == username).FirstOrDefault()?.WithoutPassword();
    }

    /// <summary>
    /// Obtient un <see cref="User"/>, basé sur l'email fourni.
    /// </summary>
    /// <param name="email">L'email à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see>.</returns>
    public User GetByEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
      {
        throw new ArgumentNullException(nameof(email));
      }

      return Entities.Find(elm => elm.Email == email).FirstOrDefault()?.WithoutPassword();
    }

    /// <summary>
    /// Enregistre un nouvel <see cref="User"/>.
    /// </summary>
    /// <param name="model">L'<see cref="User"/> a créé.</param>
    /// <returns>L'utilisateur créé.</returns>
    public User Register(User model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      // Le nom d'utilisateur doit être unique.
      if (this.GetByUsername(model.Username) != null)
      {
        throw new ArgumentException((this as ILocalizedService<UserService>).GetLocalized("RegisterErrorUserUsernameAlreadyExists", model.Username));
      }
      // L'email doit être unique.
      else if (this.GetByEmail(model.Email) != null)
      {
        throw new ArgumentException((this as ILocalizedService<UserService>).GetLocalized("RegisterErrorUserEmailAlreadyExists", model.Email));
      }

      return this.Create(model)?.WithoutPassword();
    }

    public override User Create(User elm)
    {
      if (elm == null)
      {
        throw new ArgumentNullException(nameof(elm));
      }

      string hashedPassword = CryptographicHelper.GetHash(elm.Password, _appSettings.Security.HashSalt);
      elm.Password = hashedPassword;

      return base.Create(elm);
    }
  }
}
