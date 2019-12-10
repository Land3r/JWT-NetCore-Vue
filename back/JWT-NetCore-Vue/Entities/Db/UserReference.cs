namespace JWTNetCoreVue.Entities.Db
{
  using System;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// Classe UserReference.
  /// Classe permettant de représenter un utilisateur.
  /// </summary>
  public class UserReference
  {
    /// <summary>
    /// Obtient ou définit l'Id de l'utilisateur.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtient ou définit le nom d'utilisateur de l'utilisateur.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }
  }
}
