namespace JWTNetCoreVue.Entities.Db
{
  using System;
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// Classe abstraite ADbTrackedEntity.
  /// Classe permettant d'ajouter des fonctionnalitées de tracking à d'autres entitées.
  /// </summary>
  public abstract class ADbTrackedEntity : ADbEntity, IDbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le moment de la création de l'entitée.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Created { get; set; }

    /// <summary>
    /// Obtient ou définit l'utilisateur ayant effectué la création de l'entitée.
    /// </summary>
    public UserReference CreatedBy { get; set; }

    /// <summary>
    /// Obtient ou définit le moment de la mise à jour de l'entitée.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Updated { get; set; }

    /// <summary>
    /// Obtient ou définit l'utilisateur ayant effectué la mise à jour de l'entitée.
    /// </summary>
    public UserReference UpdatedBy { get; set; }
  }
}
