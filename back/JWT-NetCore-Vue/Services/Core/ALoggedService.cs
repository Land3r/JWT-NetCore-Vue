namespace JWTNetCoreVue.Services.Core
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using System;

  /// <summary>
  /// Classe abstraite <see cref="ALoggedService{TEntity}"/>.
  /// Classe permettant d'ajouter des fonctionnalitées de logger à un service.
  /// </summary>
  /// <typeparam name="TService">Le type du service.</typeparam>
  public abstract class ALoggedService<TService> : ILoggedService<TService>
  {
    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    public ILogger<TService> Logger { get; private set; }

    /// <summary>
    /// Initialise une nouvelle instance de <see cref="ALoggedService{T}"/>.
    /// </summary>
    /// <param name="T">Le type du logger à utiliser.</param>
    public ALoggedService([FromServices] ILogger<TService> logger)
    {
      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }
      else
      {
        Logger = logger;
      }
    }
  }
}
