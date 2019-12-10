namespace JWTNetCoreVue.Models.Users
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Classe <see cref="UserAuthenticateModel"/>.
  /// Classe permettant de venir s'authentifier auprès du service des Utilisateurs.
  /// </summary>
  public class UserAuthenticateModel
  {
    /// <summary>
    /// Le nom d'utilisateur de l'<see cref="User">Utilisateur</see> à authentifier.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Le mot de passe de l'<see cref="User">Utilisateur</see> à authentifier.
    /// </summary>
    [Required]
    public string Password { get; set; }
  }
}
