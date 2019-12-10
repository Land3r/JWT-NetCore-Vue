namespace JWTNetCoreVue.Services.Core
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;

  /// <summary>
  /// Classe abstraite <see cref="ALoggedLocalizedService{TService}"/>.
  /// Classe permettant d'obtenir des fonctionnalitées de logger et de localisation sur un service.
  /// </summary>
  /// <typeparam name="TService">Le type du service.</typeparam>
  public abstract class ALoggedLocalizedService<TService> : ALoggedService<TService>, ILocalizedService<TService>
  {
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="ALoggedLocalizedService{TService}"/>.
    /// </summary>
    /// <param name="logger">Le logger à utiliser.</param>
    /// <param name="localizer">Les ressouces localisées à utiliser.</param>
    public ALoggedLocalizedService(
      ILogger<TService> logger,
      [FromServices]IStringLocalizer<TService> localizer)
      : base(logger)
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
    /// Obtient les ressources localisées.
    /// </summary>
    public IStringLocalizer<TService> Localizer { get; private set; }
  }
}
