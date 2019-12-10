namespace JWTNetCoreVue.Services.Emails
{
  using JWTNetCoreVue.Entities.Emails;

  /// <summary>
  /// Interface <see cref="IEmailService"/>.
  /// Expose des méthodes pour envoyer des emails.
  /// </summary>
  public interface IEmailService
  {
    /// <summary>
    /// Envoie un email.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="subject">Le sujet de l'email.</param>
    /// <param name="body">Le body de l'email (au format html).</param>
    void Send(EmailAddress address, string subject, string body);

    /// <summary>
    /// Envoie un email, basé sur un template HTML.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="templateName">Le nom du template email.</param>
    /// <param name="values">Les valeurs à injecter dans le template.</param>
    void SendTemplate(EmailAddress address, string templateName, dynamic values);

    /// <summary>
    /// Essaie d'envoyer un email.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="subject">Le sujet de l'email.</param>
    /// <param name="body">Le body de l'email (au format html).</param>
    /// <returns>Si l'envoi d'email a reussi ou non.</returns>
    bool TrySend(EmailAddress address, string subject, string body);

    /// <summary>
    /// Essaie d'envoyer un email, basé sur un template HTML.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="templateName">Le nom du template email.</param>
    /// <param name="values">Les valeurs à injecter dans le template.</param>
    /// <returns>Si l'envoi d'email a reussi ou non.</returns>
    bool TrySendTemplate(EmailAddress address, string templateName, dynamic values);
  }
}
