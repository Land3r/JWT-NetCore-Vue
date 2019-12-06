namespace JWTNetCoreVue.Settings
{
  public class EmailSettings
  {
    /// <summary>
    /// Obtient ou définit la configuration SMTP utilisée pour l'envoi des emails.
    /// </summary>
    public SmtpSettings Smtp { get; set; }

    /// <summary>
    /// Obtient ou définit le nom d'affichage utilisé pour l'envoi des emails.
    /// </summary>
    public string FromDisplayName { get; set; }
  }
}
