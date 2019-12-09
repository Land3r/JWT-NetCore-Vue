namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe <see cref="EnvironmentSettings"/>.
  /// Classe permettant de récupérer la configuration de l'environnement de l'application.
  /// </summary>
  public class EnvironmentSettings
  {
    /// <summary>
    /// Obtient ou définit le nom de l'environement.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Obtient ou définit l'url du front.
    /// </summary>
    public string FrontUrl { get; set; }

    /// <summary>
    /// Obtient ou définit l'url du back.
    /// </summary>
    public string BackUrl { get; set; }
  }
}