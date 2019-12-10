namespace JWTNetCoreVue.Services.Core
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using JWTNetCoreVue.Entities.Db;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;

  /// <summary>
  /// Classe abstraite AMongoEntityService.
  /// Classe permettant de rajouter des interactions CRUD avec une collection de base de données.
  /// </summary>
  /// <typeparam name="TEntity">Le type de l'entitée.</typeparam>
  /// <typeparam name="TService">Le type du service.</typeparam>
  public abstract class AMongoEntityService<TEntity, TService> : ALoggedService<TService>, ICrudService<TEntity>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="AMongoEntityService{T}"/>.
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="collectionName">Le nom de la collection en base.</param>
    /// <param name="logger">Le logger à utiliser.</param>
    public AMongoEntityService(
      IOptions<AppSettings> appSettings,
      string collectionName,
      [FromServices] ILogger<TService> logger)
      : base(logger)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else if (string.IsNullOrEmpty(collectionName))
      {
        throw new ArgumentNullException(nameof(collectionName));
      }

      var client = new MongoClient(appSettings?.Value.MongoDb.ConnectionString);
      var db = client.GetDatabase(appSettings?.Value.MongoDb.DatabaseName);

      this.Entities = db.GetCollection<TEntity>(collectionName);
    }

    /// <summary>
    /// Obtient la collection des entitées en base.
    /// </summary>
    public IMongoCollection<TEntity> Entities { get; private set; }

    /// <summary>
    /// Obtient toutes les entitées de la collection.
    /// </summary>
    /// <returns>La liste de toutes les entitées.</returns>
    public virtual IEnumerable<TEntity> Get()
    {
      return this.Entities.Find(elm => true).ToEnumerable();
    }

    /// <summary>
    /// Obtient une entitée, basé sur son Id.
    /// </summary>
    /// <param name="id">L'id de l'entitée à récupérer.</param>
    /// <returns>L'entitée ou null si aucune n'a été trouvée.</returns>
    public virtual TEntity Get(Guid id)
    {
      return this.Entities.Find<TEntity>(elm => elm.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Ajoute une entitée à la collection.
    /// </summary>
    /// <param name="elm">L'entitée à ajouter à la collection.</param>
    /// <returns>L'entitée créée.</returns>
    public virtual TEntity Create(TEntity elm)
    {
      if (elm is IDbTrackedEntity)
      {
        IDbTrackedEntity elmTracked = elm as IDbTrackedEntity;
        if (elmTracked.Created == null)
        {
          (elm as IDbTrackedEntity).Created = DateTime.UtcNow;
        }

        if (elmTracked.Updated == DateTime.MinValue)
        {
          (elm as IDbTrackedEntity).Updated = (elm as IDbTrackedEntity).Created;
        }

        if (elmTracked.CreatedBy == null)
        {
          (elm as IDbTrackedEntity).CreatedBy = new UserReference()
          {
            Id = Guid.NewGuid(),
            Username = "System",
          };
        }

        if (elmTracked.UpdatedBy == null)
        {
          (elm as IDbTrackedEntity).UpdatedBy = (elm as IDbTrackedEntity).CreatedBy;
        }
      }

      // Id field is automatically populated.
      this.Entities.InsertOne(elm);

      return elm;
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public virtual ReplaceOneResult Update(TEntity elmIn)
    {
      if (elmIn is IDbTrackedEntity)
      {
        IDbTrackedEntity elmTracked = elmIn as IDbTrackedEntity;
        (elmIn as IDbTrackedEntity).Updated = DateTime.UtcNow;
        (elmIn as IDbTrackedEntity).UpdatedBy = new UserReference()
        {
          Id = Guid.NewGuid(),
          Username = "System", // TODO.
        };
      }

      return this.Entities.ReplaceOne(book => book.Id == elmIn.Id, elmIn);
    }

    /// <summary>
    /// Mets à jour une entitée dans la collection.
    /// </summary>
    /// <param name="id">L'id de l'entitée à mettre à jour.</param>
    /// <param name="elmIn">Les données de l'entitée mise à jour.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public virtual ReplaceOneResult Update(Guid id, TEntity elmIn)
    {
      if (elmIn is IDbTrackedEntity)
      {
        IDbTrackedEntity elmTracked = elmIn as IDbTrackedEntity;
        (elmIn as IDbTrackedEntity).Updated = DateTime.UtcNow;
        (elmIn as IDbTrackedEntity).UpdatedBy = new UserReference()
        {
          Id = Guid.NewGuid(),
          Username = "System", // TODO.
        };
      }

      return this.Entities.ReplaceOne(book => book.Id == id, elmIn);
    }

    /// <summary>
    /// Supprime une entitée de la collection.
    /// </summary>
    /// <param name="elmIn">L'élement à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public virtual DeleteResult Remove(TEntity elmIn)
    {
      return this.Entities.DeleteOne(book => book.Id == elmIn.Id);
    }

    /// <summary>
    /// Supprime une entitée de la collection, par son Id.
    /// </summary>
    /// <param name="id">L'id de l'élément à supprimer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    public virtual DeleteResult Remove(Guid id)
    {
      return this.Entities.DeleteOne(book => book.Id == id);
    }
  }
}
