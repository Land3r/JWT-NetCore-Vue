namespace JWTNetCoreVue.Entities.Users
{
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  public class UserPasswordResetToken : ADbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le token de réinitialisation du mot de passe d'un <see cref="User"/>
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Token { get; set; }
  }
}
