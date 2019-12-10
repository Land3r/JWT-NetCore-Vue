namespace JWTNetCoreVue.Services.Users
{
  using JWTNetCoreVue.Entities.Users;
  using JWTNetCoreVue.Services.Core;

  /// <summary>
  /// Interface <see cref="IUserPasswordResetTokenService"/>.
  /// Service permettant un utilisateur de gérer les tokens de réinitilisations de mot de passes.
  /// </summary>
  public interface IUserPasswordResetTokenService : ICrudService<UserPasswordResetToken>
  {
    /// <summary>
    /// Obtient le token de réinitialisation du mot de passe, via le token fourni.
    /// </summary>
    /// <param name="token">La valeur du token a trouver.</param>
    /// <returns>Le <see cref="UserPasswordResetToken"/> si trouvé.</returns>
    UserPasswordResetToken GetByToken(string token);

    /// <summary>
    /// Retourne si le <see cref="UserPasswordResetToken"/> fourni est valide.
    /// </summary>
    /// <param name="userPasswordResetToken">Le <see cref="UserPasswordResetToken"/> à valider.</param>
    /// <returns>Si le <see cref="UserPasswordResetToken"/> est valide ou non.</returns>
    bool IsValid(UserPasswordResetToken userPasswordResetToken);
  }
}