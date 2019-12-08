namespace JWTNetCoreVue.Entities.Emails
{
  using JWTNetCoreVue.Entities.Db;

  /// <summary>
  /// Classe <see cref="EmailTemplate"/>.
  /// Permet de générer des emails dynamiquement, basé sur du remplacement de token.
  /// </summary>
  public class EmailTemplate : ADbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le nom du template email.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Obtient ou définit le template du subject de l'email à génerer.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Obtient ou définit le template du body de l'email a génerer.
    /// </summary>
    public string Body { get; set; }
  }
}
