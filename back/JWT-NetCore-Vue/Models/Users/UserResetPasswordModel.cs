namespace JWTNetCoreVue.Models.Users
{
  /// <summary>
  /// Classe <see cref="UserResetPasswordModel"/>.
  /// Modèle pour demander une réinitialisation de mot de passe d'une compte utilisateur.
  /// </summary>
  public class UserResetPasswordModel
  {
    /// <summary>
    /// Obtient ou définit le token de réinitialisation du mot de passe.
    /// </summary>
    public string ResetPasswordToken { get; set; }

    /// <summary>
    /// Obtient ou définit l'email du compte à réinitialiser.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Obtient ou définit le nom d'utilisateur du compte à réinitialiser.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Obtient ou définit le nouveau mot de passe du compte.
    /// </summary>
    public string Password { get; set; }
  }
}
