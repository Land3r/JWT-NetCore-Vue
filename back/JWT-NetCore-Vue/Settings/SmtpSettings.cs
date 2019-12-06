namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe SmtpSettings.
  /// Classe permettant d'obtenir la configuration utilisée pour se connecter auprès du SMTP.
  public class SmtpSettings
  {
    /// <summary>
    /// Obtient ou définit l'Url du serveur SMTP utilisé pour l'envoi d'emails.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Obtient ou définit le Port du serveur SMTP utilisé pour l'envoi d'emails.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Obtient ou définit le nom d'utilisateur utilisé pour s'authentifier auprès du serveur SMTP.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Obtient ou définit le mot de passe utilisé pour s'authentifier auprès du serveur SMTP.
    /// </summary>
    public string Password { get; set; }
  }
}
