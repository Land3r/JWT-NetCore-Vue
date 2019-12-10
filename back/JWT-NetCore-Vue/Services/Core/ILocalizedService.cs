namespace JWTNetCoreVue.Services.Core
{
  using System;
  using System.Globalization;
  using Microsoft.Extensions.Localization;

  /// <summary>
  /// Interface <see cref="ILocalizedService{T}"/>.
  /// Interface permettant à une classe d'accéder à des ressources localisées.
  /// </summary>
  /// <typeparam name="T">Le type du service à localiser.</typeparam>
  public interface ILocalizedService<T>
  {
    /// <summary>
    /// Obtient les ressources localisées.
    /// </summary>
    IStringLocalizer<T> Localizer { get; }

    /// <summary>
    /// Obtient la ressource texte demandée, dans la culture courante.
    /// </summary>
    /// <param name="key">Le nom de la clé de ressource.</param>
    /// <returns>Le texte correspondant à cette clé, dans la culture en cours.</returns>
    public string GetLocalized(string key)
    {
      string result = this.Localizer[key]?.Value;

      if (result == null)
      {
        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "La clé {0} n'existe pas dans le fichier de ressource.", key));
      }
      else
      {
        return result;
      }
    }

    /// <summary>
    /// Obtient la ressource texte demandée, dans la culture courante.
    /// </summary>
    /// <param name="key">Le nom de la clé de ressource.</param>
    /// <param name="args">Les arguments à passer au string.Format.</param>
    /// <returns>Le texte correspondant à cette clé, dans la culture en cours.</returns>
    public string GetLocalized(string key, params string[] args)
    {
      string result = this.Localizer[key]?.Value;

      if (result == null)
      {
        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "La clé {0} n'existe pas dans le fichier de ressource.", key));
      }
      else
      {
        return string.Format(CultureInfo.InvariantCulture, result, args);
      }
    }
  }
}
