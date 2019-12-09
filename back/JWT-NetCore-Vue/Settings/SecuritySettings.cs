namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe <see cref="SecuritySettings"/>.
  /// Classe permettant de récupérer les configurations sécuritaires de l'application.
  /// </summary>
  public class SecuritySettings
  {
    /// <summary>
    /// Obtient ou définit le hash utilisé dans la génération des hashs.
    /// </summary>
    public string HashSalt { get; set; }

    /// <summary>
    /// Obtient ou définit la durée de validitée du token de réinitialisation du mot de passe.
    /// </summary>
    public int ResetPasswordTokenDurationInMinutes { get; set; }

    /// <summary>
    /// Obtient ou définit la configuration utilisée pour configurer l'authentification JWT.
    /// </summary>
    public JWTSettings JWT { get; set; }
  }
}