namespace JWTNetCoreVue.Entities
{
  using System;

  /// <summary>
  /// Représente un utilisateur.
  /// </summary>
  public class User
  {
    /// <summary>
    /// Obtient ou définit l'Id de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtient ou définit le Prénom de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Obtient ou définit de Nom de famille de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Obtient ou définit le Nom d'utilisateur de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Obtient ou définit l'Email de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Obtient ou définit le Mot de passe de l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Obtient ou définit le Token d'authentification utilisé par l'<see cref="User">Utilisateur</see>.
    /// </summary>
    public string Token { get; set; }
  }
}
