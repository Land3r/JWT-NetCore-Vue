namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe AppSettings.
  /// Classe permettant de récupérer les valeurs de configuration de l'application.
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Obtient ou définit la configuration de l'environnement.
    /// </summary>
    public EnvironmentSettings Environment { get; set; }

    /// <summary>
    /// Obtient ou définit la configuration utilisée pour configurer la connection à la base de données.
    /// </summary>
    public MongoDbSettings MongoDb { get; set; }

    /// <summary>
    /// Obtient ou définit la configuration utilisée pour envoyer des emails.
    /// </summary>
    public EmailSettings Email { get; set; }

    /// <summary>
    /// Obtient ou définit la configuration utilisée pour les aspects sécuritaires.
    /// </summary>
    public SecuritySettings Security { get; set; }
  }
}
