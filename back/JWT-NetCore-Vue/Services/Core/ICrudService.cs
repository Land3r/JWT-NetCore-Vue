namespace JWTNetCoreVue.Services.Core
{
  using System;
  using System.Collections.Generic;
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Driver;

  /// <summary>
  /// Interface <see cref="ICrudService{TEntity}"/>.
  /// Interface permettant d'accéder aux fonctionnalitées CRUD du service.
  /// </summary>
  /// <typeparam name="TEntity">LE type d'entitée manipulée par le service.</typeparam>
  public interface ICrudService<TEntity>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Obtient la collection des entitées en base.
    /// </summary>
    IMongoCollection<TEntity> Entities { get; }

    /// <summary>
    /// Obtient toutes les entitées de la collection.
    /// </summary>
    /// <returns>La liste de toutes les entitées.</returns>
    public IEnumerable<TEntity> Get();

    /// <summary>
    /// Obtient une entitée, basé sur son Id.
    /// </summary>
    /// <param name="id">L'id de l'entitée à récupérer.</param>
    /// <returns>L'entitée ou null si aucune n'a été trouvée.</returns>
    public TEntity Get(Guid id);

    /// <summary>
    /// Ajoute une entitée à la collection.
    /// </summary>
    /// <param name="elm">L'entitée à ajouter à la collection.</param>
    /// <returns>L'entitée créée.</returns>
    public TEntity Create(TEntity elm);

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public ReplaceOneResult Update(TEntity elmIn);

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="id">L'id de l'entitée à mettre à jour.</param>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public ReplaceOneResult Update(Guid id, TEntity elmIn);

    /// <summary>
    /// Supprime une entitée de la collection.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(TEntity elmIn);

    /// <summary>
    /// Supprime une entitée de la collection, par son Id.
    /// </summary>
    /// <param name="id">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(Guid id);
  }
}
