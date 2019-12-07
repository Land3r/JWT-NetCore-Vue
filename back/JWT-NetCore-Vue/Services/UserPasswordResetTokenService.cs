using JWTNetCoreVue.Entities.Users;
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

namespace JWTNetCoreVue.Services
{
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
      return Entities.Find(elm => elm.Token == token).FirstOrDefault();
    }

    public bool IsValid(UserPasswordResetToken userPasswordResetToken)
    {
      //TODO: Vérifier que l'Id existe && que le temps écoulé depuis la génératione est < à une valeur à determiner.
      return true;
    }
  }
}
