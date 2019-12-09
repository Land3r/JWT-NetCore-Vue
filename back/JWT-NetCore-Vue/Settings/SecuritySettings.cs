namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe <see cref="SecuritySettings"/>.
  /// Classe permettant de récupérer les configurations sécuritaires de l'application.
  /// </summary>
  public class SecuritySettings
  {
    /// <summary>
    /// Obtient ou définit le hash utilisé dans la génération des hashs.
    /// </summary>
    public string HashSalt { get; set; }
  }
}