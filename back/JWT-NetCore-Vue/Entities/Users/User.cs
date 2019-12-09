namespace JWTNetCoreVue.Entities.Users
{
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// Représente un utilisateur.
  /// </summary>
  public class User : ADbEntity
  {
    /// <summary>
    /// Obtient ou définit le Prénom de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string FirstName { get; set; }

    /// <summary>
    /// Obtient ou définit de Nom de famille de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string LastName { get; set; }

    /// <summary>
    /// Obtient ou définit le Nom d'utilisateur de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }

    /// <summary>
    /// Obtient ou définit l'Email de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }

    /// <summary>
    /// Obtient ou définit le Mot de passe de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Password { get; set; }

    /// <summary>
    /// Obtient ou définit si le compte utilisateur est actif ou non.
    /// </summary>
    [BsonRepresentation(BsonType.Boolean)]
    public bool? Active { get; set; }

    /// <summary>
    /// Obtient ou définit le token d'activation du compte.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string ActivationToken { get; set; }

    /// <summary>
    /// Obtient ou définit le Token d'authentification utilisé par l'<see cref="User">Utilisateur</see>.
    /// </summary>
    [BsonIgnore]
    public string Token { get; set; }
  }
}
