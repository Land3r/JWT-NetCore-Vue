namespace JWTNetCoreVue.Services.Users
{
  using System;
  using JWTNetCoreVue.Entities.Users;
  using JWTNetCoreVue.Models.Users;
  using JWTNetCoreVue.Services.Core;
  using MongoDB.Driver;

  /// <summary>
  /// Interface IUSerService.
  /// Interface pour le service des utilisateurs.
  /// </summary>
  public interface IUserService : ICrudService<User>
  {
    /// <summary>
    /// Authentifie un <see cref="User"/>, basé sur le <see cref="UserAuthenticateModel"/> fourni.
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> authentifié.</returns>
    User Authenticate(UserAuthenticateModel model);

    /// <summary>
    /// Obtient un <see cref="User"/>, basé sur le username fourni.
    /// </summary>
    /// <param name="username">Le nom d'utilisateur à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see>.</returns>
    User GetByUsername(string username);

    /// <summary>
    /// Obtient un <see cref="User"/>, basé sur l'email fourni.
    /// </summary>
    /// <param name="email">L'email à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see>.</returns>
    User GetByEmail(string email);

    /// <summary>
    /// Mets à jour un utilisateur avec un nouveau mot de passe.
    /// </summary>
    /// <param name="id">L'id de l'utilisateur à mettre à jour.</param>
    /// <param name="clearPassword">Le mot de passe en clair, avant encryption.</param>
    /// <returns>Le résultat de la mise à jour.</returns>
    ReplaceOneResult UpdatePassword(Guid id, string clearPassword);

    /// <summary>
    /// Enregistre un nouvel <see cref="User"/>.
    /// </summary>
    /// <param name="model">L'<see cref="User"/> a créé.</param>
    /// <returns>L'utilisateur créé.</returns>
    User Register(User model);

    /// <summary>
    /// Rends actif un utilisateur.
    /// </summary>
    /// <param name="token">Le token d\'activation de l'utilisateur a activer.</param>
    /// <returns>L'utilisateur activé.</returns>
    User Activate(string token);
  }
}
