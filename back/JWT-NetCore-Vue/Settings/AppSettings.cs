namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe AppSettings.
  /// Classe permettant de récupérer les valeurs de configuration de l'application.
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Obtient ou définit la configuration utilisée pour configurer l'authentification JWT.
    /// </summary>
    public JWTSettings JWT { get; set; }

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
