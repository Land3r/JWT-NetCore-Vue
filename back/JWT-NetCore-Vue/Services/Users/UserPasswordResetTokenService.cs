namespace JWTNetCoreVue.Services.Users
{
  using System;
  using System.Linq;
  using JWTNetCoreVue.Entities.Users;
  using JWTNetCoreVue.Helpers;
  using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;

  /// <summary>
  /// Classe <see cref="UserPasswordResetTokenService"/>.
  /// Classe permettant à un utilisateur de demander a réinitialiser son mot de passe.
  /// </summary>
  public class UserPasswordResetTokenService : AMongoEntityLocalizedService<UserPasswordResetToken, UserPasswordResetTokenService>, IUserPasswordResetTokenService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string CollectionName = "UserPasswordResetToken";

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="UserPasswordResetTokenService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public UserPasswordResetTokenService(
      [FromServices]IStringLocalizer<UserPasswordResetTokenService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<UserPasswordResetTokenService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }

    /// <summary>
    /// Obtient un <see cref="UserPasswordResetToken"/>, basé sur la valeur du token.
    /// Notez que le token est hashé avant d'être comparé en base.
    /// </summary>
    /// <param name="token">La valeur du token à rechercher.</param>
    /// <returns>Le <see cref="UserPasswordResetToken"/>, si trouvé.</returns>
    public UserPasswordResetToken GetByToken(string token)
    {
      if (string.IsNullOrEmpty(token))
      {
        throw new ArgumentNullException(nameof(token));
      }

      // We are using a hash function for storing reset password token for security reasons.
      // Basically it makes it more difficult for an attacker with a read access to the database to gain access to the user account.
      // See https://security.stackexchange.com/questions/86913/should-password-reset-tokens-be-hashed-when-stored-in-a-database for more info.
      string hash = CryptographicHelper.GetHash(token);

      return this.Entities.Find(elm => elm.Token == hash).FirstOrDefault();
    }

    /// <summary>
    /// Crée un <see cref="UserPasswordResetToken"/>, avec gestion du hashage en base.
    /// </summary>
    /// <param name="elm">Le <see cref="UserPasswordResetToken"/> à créer.</param>
    /// <returns>L'élément créé.</returns>
    public override UserPasswordResetToken Create(UserPasswordResetToken elm)
    {
      // We are using a hash function for storing reset password token for security reasons.
      // Basically it makes it more difficult for an attacker with a read access to the database to gain access to the user account.
      // See https://security.stackexchange.com/questions/86913/should-password-reset-tokens-be-hashed-when-stored-in-a-database for more info.
      string hash = CryptographicHelper.GetHash(elm?.Token);
      elm.Token = hash;

      return base.Create(elm);
    }

    /// <summary>
    /// Obtient si le token de réinitialisation de mot de passe est valide.
    /// </summary>
    /// <param name="userPasswordResetToken">Le token a valider.</param>
    /// <returns>Si le token est valide ou non.</returns>
    public bool IsValid(UserPasswordResetToken userPasswordResetToken)
    {
      if (userPasswordResetToken == null)
      {
        throw new ArgumentNullException(nameof(userPasswordResetToken));
      }

      if (userPasswordResetToken.ValidUntil > DateTime.UtcNow)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
