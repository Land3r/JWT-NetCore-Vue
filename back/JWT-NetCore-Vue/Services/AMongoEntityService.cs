namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities.Db;
  using JWTNetCoreVue.Settings;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// Classe abstraite AMongoEntityService.
  /// Classe permettant de rajouter des interactions CRUD avec une collection de base de données.
  /// </summary>
  /// <typeparam name="T">Le type de l'entitée.</typeparam>
  public abstract class AMongoEntityService<T> : ICrudService<T> where T : IDbEntity
  {
    /// <summary>
    /// La collection des entitées en base.
    /// </summary>
    protected readonly IMongoCollection<T> _entities = null;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="AMongoEntityService{T}"/>
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="collectionName">Le nom de la collection en base.</param>
    public AMongoEntityService(IOptions<AppSettings> appSettings, string collectionName)
    {
      var client = new MongoClient(appSettings?.Value.MongoDbSettings.ConnectionString);
      var db = client.GetDatabase(appSettings?.Value.MongoDbSettings.DatabaseName);

      _entities = db.GetCollection<T>(collectionName);
    }

    /// <summary>
    /// Obtient toutes les entitées de la collection.
    /// </summary>
    /// <returns>La liste de toutes les entitées.</returns>
    public IEnumerable<T> Get()
    {
      return _entities.Find(elm => true).ToEnumerable();
    }

    /// <summary>
    /// Obtient une entitée, basé sur son Id.
    /// </summary>
    /// <param name="id">L'id de l'entitée à récupérer.</param>
    /// <returns>L'entitée ou null si aucune n'a été trouvée.</returns>
    public T Get(Guid id)
    {
      return _entities.Find<T>(elm => elm.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Ajoute une entitée à la collection.
    /// </summary>
    /// <param name="elm">L'entitée à ajouter à la collection.</param>
    /// <returns>L'entitée créée.</returns>
    public T Create(T elm)
    {
      _entities.InsertOne(elm);
      // Id field is automatically populated.
      return elm;
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(T elmIn)
    {
      return _entities.ReplaceOne(book => book.Id == elmIn.Id, elmIn);
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="id">L'id de l'entitée à mettre à jour.</param>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    public ReplaceOneResult Update(Guid id, T elmIn)
    {
      return _entities.ReplaceOne(book => book.Id == id, elmIn);
    }

    /// <summary>
    /// Supprime une entitée de la collection.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(T elmIn)
    {
      return _entities.DeleteOne(book => book.Id == elmIn.Id);
    }

    /// <summary>
    /// Supprime une entitée de la collection, par son Id.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public DeleteResult Remove(Guid id)
    {
      return _entities.DeleteOne(book => book.Id == id);
    }
  }
}
