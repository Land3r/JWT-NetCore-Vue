namespace JWTNetCoreVue.Models.Users
{
  /// <summary>
  /// Classe <see cref="UserAuthenticateModel"/>.
  /// Modèle pour demander une réinitialisation du mot de passe d'un compte.
  /// </summary>
  public class UserPasswordLostModel
  {
    /// <summary>
    /// Obtient ou définit le nom d'utilisateur du compte utilisateur dont le mot de passe doit être réinitialiser.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Obtient ou définit l'email du compte utilisateur dont le mot de passe doit être réinitialiser.
    /// </summary>
    public string Email { get; set; }
  }
}
