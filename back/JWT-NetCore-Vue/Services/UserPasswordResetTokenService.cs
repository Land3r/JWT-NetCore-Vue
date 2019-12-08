namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities.Users;
    using JWTNetCoreVue.Helpers;
    using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  /// <summary>
  /// Classe <see cref="UserPasswordResetTokenService"/>.
  /// Classe permettant à un utilisateur de demander a réinitialiser son mot de passe.
  /// </summary>
  public class UserPasswordResetTokenService : AMongoEntityLocalizedService<UserPasswordResetToken, UserPasswordResetTokenService>, IUserPasswordResetTokenService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string _collectionName = "UserPasswordResetToken";

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="UserPasswordResetTokenService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public UserPasswordResetTokenService([FromServices]IStringLocalizer<UserPasswordResetTokenService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<UserPasswordResetTokenService> logger) : base(appSettings, _collectionName, logger, localizer)
    {
    }

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

      return Entities.Find(elm => elm.Token == hash).FirstOrDefault();
    }

    public override UserPasswordResetToken Create(UserPasswordResetToken elm)
    {
      // We are using a hash function for storing reset password token for security reasons.
      // Basically it makes it more difficult for an attacker with a read access to the database to gain access to the user account.
      // See https://security.stackexchange.com/questions/86913/should-password-reset-tokens-be-hashed-when-stored-in-a-database for more info.
      string hash = CryptographicHelper.GetHash(elm?.Token);
      elm.Token = hash;

      return base.Create(elm);
    }

    public bool IsValid(UserPasswordResetToken userPasswordResetToken)
    {
      //TODO: Vérifier que l'Id existe && que le temps écoulé depuis la génération est < à une valeur à determiner.
      return true;
    }
  }
}
