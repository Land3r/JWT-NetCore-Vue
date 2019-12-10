namespace JWTNetCoreVue.Services.Core
{
  using System;
  using JWTNetCoreVue.Entities.Db;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  /// <summary>
  /// Classe abstraite <see cref="AMongoEntityLocalizedService{TEntity, TService}"/>.
  /// Classe permettant d'utiliser un service de localisation de ressources, en plus du CRUD sur une entitée.
  /// </summary>
  /// <typeparam name="TEntity">Le type de l'entitée.</typeparam>
  /// <typeparam name="TService">Le type du service.</typeparam>
  public abstract class AMongoEntityLocalizedService<TEntity, TService> : AMongoEntityService<TEntity, TService>, ILocalizedService<TService>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="AMongoEntityService{T}"/>.
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="collectionName">Le nom de la collection en base.</param>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="logger">Le logger utilisé.</param>
    public AMongoEntityLocalizedService(
      IOptions<AppSettings> appSettings,
      string collectionName,
      [FromServices] ILogger<TService> logger,
      [FromServices]IStringLocalizer<TService> localizer)
      : base(appSettings, collectionName, logger)
    {
      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }
      else
      {
        this.Localizer = localizer;
      }
    }

    /// <summary>
    /// Les ressources de langue.
    /// </summary>
    public IStringLocalizer<TService> Localizer { get; private set; }
  }
}
