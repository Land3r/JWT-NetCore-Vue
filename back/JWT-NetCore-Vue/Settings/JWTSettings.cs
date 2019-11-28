namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe JWTSettings.
  /// Classe permettant d'obtenir la configuration utilisée pour configurer l'authentification JWT.
  /// </summary>
  public class JWTSettings
  {
    /// <summary>
    /// Obtient ou définit le Secret utilisé pour le JWT.
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Obtient ou définit la durée de validitée du Token JWT après sa génération.
    /// </summary>
    public int DurationInDays { get; set; }
  }
}
