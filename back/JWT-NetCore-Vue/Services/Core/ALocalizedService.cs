namespace JWTNetCoreVue.Services.Core
{
  using System;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;

  /// <summary>
  /// Classe abstraite <see cref="ALocalizedService{T}"/>.
  /// </summary>
  /// <typeparam name="T">Le type sous jaccent du namespace a localiser.</typeparam>
  public abstract class ALocalizedService<T> : ILocalizedService<T>
  {
    /// <summary>
    /// Initialise une nouvelle instance de <see cref="ALocalizedService{T}"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    public ALocalizedService([FromServices] IStringLocalizer<T> localizer)
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
    public IStringLocalizer<T> Localizer { get; private set; }
  }
}
