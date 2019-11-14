namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities;
  using JWTNetCoreVue.Models;

  /// <summary>
  /// Interface IUSerService.
  /// Interface pour le service des utilisateurs.
  /// </summary>
  public interface IUserService
  {
    /// <summary>
    /// Authentifie un <see cref="User"/>, basé sur le <see cref="UserAuthenticateModel"/> fourni.
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> authentifié.</returns>
    User Authenticate(UserAuthenticateModel model);

    /// <summary>
    /// Obtient l'<see cref="User">Utilisateur</see> authentifié.
    /// </summary>
    /// <returns>L'<see cref="User">Utilisateur</see> en cours.</returns>
    User GetCurrentUser();
  }
}
