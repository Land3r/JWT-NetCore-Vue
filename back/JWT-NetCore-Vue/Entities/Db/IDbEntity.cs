namespace JWTNetCoreVue.Entities.Db
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;
  using System;

  /// <summary>
  /// Interface IDbEntity.
  /// Interface permettant de représenter une entitée en base.
  /// </summary>
  public interface IDbEntity
  {
    /// <summary>
    /// Obtient ou définit l'id de l'entitée.
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }
  }
}
