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
    /// Obtient ou définir la configuration utilisée pour configurer la connection à la base de données.
    /// </summary>
    public MongoDbSettings MongoDbSettings { get; set; }
  }
}
