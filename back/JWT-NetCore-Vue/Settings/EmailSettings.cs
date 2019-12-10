namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe <see cref="EmailSettings"/>.
  /// Classe de configuration pour les emails de l'applications.
  /// </summary>
  public class EmailSettings
  {
    /// <summary>
    /// Obtient ou définit la configuration SMTP utilisée pour l'envoi des emails.
    /// </summary>
    public SmtpSettings Smtp { get; set; }

    /// <summary>
    /// Obtient ou définit les informations de l'email envoyant l'email.
    /// </summary>
    public EmailFromSettings From { get; set; }
  }
}
