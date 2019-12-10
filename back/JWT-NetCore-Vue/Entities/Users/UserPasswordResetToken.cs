namespace JWTNetCoreVue.Entities.Users
{
  using System;
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// Classe <see cref="UserPasswordResetToken"/>.
  /// Permet de demander une réinitialisation d'un compte utilisateur.
  /// </summary>
  public class UserPasswordResetToken : ADbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le token de réinitialisation du mot de passe d'un <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Token { get; set; }

    /// <summary>
    /// Obtient ou définit la date jusqu'à laquelle le token est valide.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime ValidUntil { get; set; }
  }
}
