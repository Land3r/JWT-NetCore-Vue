namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe MongoDbSettings.
  /// Classe permettant d'obtenir la configuration utilisée pour configurer la connection à la base de données MongoDb.
  /// </summary>
  public class MongoDbSettings
  {
    /// <summary>
    /// Obtient ou définit la chaine de connection au serveur de base de données.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Obtient ou définit le nom de la base de données à utiliser.
    /// </summary>
    public string DatabaseName { get; set; }
  }
}
