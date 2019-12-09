namespace JWTNetCoreVue.Entities.Emails
{
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// Classe <see cref="EmailTemplate"/>.
  /// Permet de générer des emails dynamiquement, basé sur du remplacement de token.
  /// </summary>
  public class EmailTemplate : ADbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le nom du template email.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    /// <summary>
    /// Obtient ou définit le template du subject de l'email à génerer.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Subject { get; set; }

    /// <summary>
    /// Obtient ou définit le template du body de l'email a génerer.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Body { get; set; }
  }
}
