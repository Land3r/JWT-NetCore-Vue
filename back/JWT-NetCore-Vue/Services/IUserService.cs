﻿namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities;
  using JWTNetCoreVue.Models;

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
    User Get(string username);
  }
}