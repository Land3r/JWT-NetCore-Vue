namespace JWTNetCoreVue.Models.Users
{
  /// <summary>
  /// Classe <see cref="UserPasswordLostResponseModel"/>.
  /// Modèle de réponse pour la demande de réinitialisation du mot de passe.
  /// </summary>
  public class UserPasswordLostResponseModel
  {
    /// <summary>
    /// Obtient ou définit l'email de l'utilisateur.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Obtient ou définit le nom de l'utilisateur.
    /// </summary>
    public string Username { get; set; }
  }
}
