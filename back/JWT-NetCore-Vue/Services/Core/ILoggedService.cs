namespace JWTNetCoreVue.Services.Core
{
  using Microsoft.Extensions.Logging;

  /// <summary>
  /// Interface ILoggedService.
  /// Permet d'acceder aux fonctionnalitées de log d'un service.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface ILoggedService<T>
  {
    /// <summary>
    /// Obtient l'instance du logger
    /// </summary>
    ILogger<T> Logger { get; }
  }
}