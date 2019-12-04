namespace JWTNetCoreVue.Services.Core
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using System;

  public abstract class ALoggedService<T> : ILoggedService<T>
  {
    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    public ILogger<T> Logger { get; private set; }

    /// <summary>
    /// Initialise une nouvelle instance de <see cref="ALoggedService{T}"/>.
    /// </summary>
    /// <param name="T">Le type du logger à utiliser.</param>
    public ALoggedService([FromServices] ILogger<T> logger)
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
