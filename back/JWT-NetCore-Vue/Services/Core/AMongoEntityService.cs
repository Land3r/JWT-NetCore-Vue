﻿namespace JWTNetCoreVue.Services.Core
{
  using JWTNetCoreVue.Entities.Db;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// Classe abstraite AMongoEntityService.
  /// Classe permettant de rajouter des interactions CRUD avec une collection de base de données.
  /// </summary>
  /// <typeparam name="TEntity">Le type de l'entitée.</typeparam>
  /// <typeparam name="TService">Le type du service.</typeparam>
  public abstract class AMongoEntityService<TEntity, TService> : ALoggedService<TService>, ICrudService<TEntity> where TEntity : IDbEntity
  {
    /// <summary>
    /// Obtient la collection des entitées en base.
    /// </summary>
    public IMongoCollection<TEntity> Entities { get; private set; }

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="AMongoEntityService{T}"/>
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="collectionName">Le nom de la collection en base.</param>
    public AMongoEntityService(IOptions<AppSettings> appSettings, string collectionName, [FromServices] ILogger<TService> logger) : base(logger)
    {
      var client = new MongoClient(appSettings?.Value.MongoDb.ConnectionString);
      var db = client.GetDatabase(appSettings?.Value.MongoDb.DatabaseName);

      Entities = db.GetCollection<TEntity>(collectionName);
    }

    /// <summary>
    /// Obtient toutes les entitées de la collection.
    /// </summary>
    /// <returns>La liste de toutes les entitées.</returns>
    public IEnumerable<TEntity> Get()
    {
      return Entities.Find(elm => true).ToEnumerable();
    }

    /// <summary>
    /// Obtient une entitée, basé sur son Id.
    /// </summary>
    /// <param name="id">L'id de l'entitée à récupérer.</param>
    /// <returns>L'entitée ou null si aucune n'a été trouvée.</returns>
    public TEntity Get(Guid id)
    {
      return Entities.Find<TEntity>(elm => elm.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Ajoute une entitée à la collection.
    /// </summary>
    /// <param name="elm">L'entitée à ajouter à la collection.</param>
    /// <returns>L'entitée créée.</returns>
    public TEntity Create(TEntity elm)
    {
      Entities.InsertOne(elm);
      // Id field is automatically populated.
      return elm;
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(TEntity elmIn)
    {
      return Entities.ReplaceOne(book => book.Id == elmIn.Id, elmIn);
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="id">L'id de l'entitée à mettre à jour.</param>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(Guid id, TEntity elmIn)
    {
      return Entities.ReplaceOne(book => book.Id == id, elmIn);
    }

    /// <summary>
    /// Supprime une entitée de la collection.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(TEntity elmIn)
    {
      return Entities.DeleteOne(book => book.Id == elmIn.Id);
    }

    /// <summary>
    /// Supprime une entitée de la collection, par son Id.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(Guid id)
    {
      return Entities.DeleteOne(book => book.Id == id);
    }
  }
}