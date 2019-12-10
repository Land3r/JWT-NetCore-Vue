namespace JWTNetCoreVue.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using JWTNetCoreVue.Entities.Users;

  /// <summary>
  /// Collection de méthodes d'extensions pour la gestion des <see cref="User">Utilisateurs</see>.
  /// </summary>
  public static class UserExtension
  {
    /// <summary>
    /// Retire les mots de passe d'une collection d'<see cref="User">Utilisateurs</see>.
    /// </summary>
    /// <param name="users">La collection d'<see cref="User">Utilisateurs</see>.</param>
    /// <returns>La collection d'<see cref="User">Utilisateurs</see> sans les mots de passe.</returns>
    public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
    {
      if (users == null)
      {
        throw new ArgumentNullException(nameof(users));
      }
      else
      {
        return users.Select(x => x.WithoutPassword());
      }
    }

    /// <summary>
    /// Retire le mot de passe d'un <see cref="User">Utilisateur</see>.
    /// </summary>
    /// <param name="user">L'<see cref="User">Utilisateur</see>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> sans le mots de passe.</returns>
    public static User WithoutPassword(this User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }
      else
      {
        user.Password = null;
        return user;
      }
    }
  }
}
