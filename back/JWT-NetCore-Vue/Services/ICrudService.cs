﻿namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities.Db;
  using MongoDB.Driver;
  using System;
  using System.Collections.Generic;

  public interface ICrudService<T> where T : IDbEntity
  {
    /// <summary>
    /// Obtient toutes les entitées de la collection.
    /// </summary>
    /// <returns>La liste de toutes les entitées.</returns>
    public IEnumerable<T> Get();

    /// <summary>
    /// Obtient une entitée, basé sur son Id.
    /// </summary>
    /// <param name="id">L'id de l'entitée à récupérer.</param>
    /// <returns>L'entitée ou null si aucune n'a été trouvée.</returns>
    public T Get(Guid id);

    /// <summary>
    /// Ajoute une entitée à la collection.
    /// </summary>
    /// <param name="elm">L'entitée à ajouter à la collection.</param>
    /// <returns>L'entitée créée.</returns>
    public T Create(T elm);

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(T elmIn);

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="id">L'id de l'entitée à mettre à jour.</param>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(Guid id, T elmIn);

    /// <summary>
    /// Supprime une entitée de la collection.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(T elmIn);

    /// <summary>
    /// Supprime une entitée de la collection, par son Id.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(Guid id);
  }
}
