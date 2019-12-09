namespace JWTNetCoreVue.Entities.Db
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

  /// <summary>
  /// Classe abstraite ADbEntity.
  /// Classe permettant de rajouter une identitée unique à une entitée dans la base.
  /// </summary>
  public abstract class ADbEntity : IDbEntity
  {
    /// <summary>
    /// Obtient ou définit l'id de l'entitée.
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }
  }
}
