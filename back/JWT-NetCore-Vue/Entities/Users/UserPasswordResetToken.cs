namespace JWTNetCoreVue.Entities.Users
{
  using JWTNetCoreVue.Entities.Db;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public class UserPasswordResetToken : ADbTrackedEntity
  {
    /// <summary>
    /// Obtient ou définit le token de réinitialisation du mot de passe d'un <see cref="User"/>
    /// </summary>
    public string Token { get; set; }
  }
}
