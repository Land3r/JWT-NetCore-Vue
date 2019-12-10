namespace JWTNetCoreVue.Services.Emails
{
  using System;
  using JWTNetCoreVue.Entities.Emails;
  using JWTNetCoreVue.Extensions;
  using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using MailKit.Net.Smtp;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MimeKit;
  using MimeKit.Text;

  /// <summary>
  /// Classe <see cref="EmailService"/>.
  /// Service permettant de gérer les échanges emails de l'application.
  /// </summary>
  public class EmailService : ALoggedLocalizedService<EmailService>, IEmailService, IDisposable
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// Le service des templates emails.
    /// </summary>
    private readonly IEmailTemplateService emailTemplateService;

    /// <summary>
    /// L'instance du client smtp.
    /// </summary>
    private readonly SmtpClient smtpClient = new SmtpClient();

    /// <summary>
    /// Initialise une nouvelle instance de <see cref="EmailService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources localisées.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="emailTemplateService">Le service de template emails.</param>
    /// <param name="logger">Le logger utilisé.</param>
    public EmailService(
      [FromServices]IStringLocalizer<EmailService> localizer,
      IOptions<AppSettings> appSettings,
      IEmailTemplateService emailTemplateService,
      [FromServices] ILogger<EmailService> logger)
      : base(logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings.Value;
      }

      if (emailTemplateService == null)
      {
        throw new ArgumentNullException(nameof(emailTemplateService));
      }
      else
      {
        this.emailTemplateService = emailTemplateService;
      }
    }

    /// <summary>
    /// Destructeur de l'instance de la classe.
    /// </summary>
    ~EmailService()
    {
      this.Dispose(false);
    }

    /// <summary>
    /// Obtient si le service est connecté au smtp.
    /// </summary>
    private bool IsConnected
    {
      get { return this.smtpClient.IsConnected; }
    }

    /// <summary>
    /// Envoie un email, basé sur un template HTML.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="templateName">Le nom du template email.</param>
    /// <param name="values">Les valeurs à injecter dans le template.</param>
    public void SendTemplate(EmailAddress address, string templateName, dynamic values)
    {
      if (string.IsNullOrEmpty(templateName))
      {
        throw new ArgumentNullException(nameof(templateName));
      }

      EmailTemplate emailTemplate = this.emailTemplateService.GetByName(templateName);
      if (emailTemplate == null)
      {
        throw new ApplicationException($"EmailTemplate nammed {templateName} not found.");
      }

      // We need to call explicitly the extension method because it uses dynamic type parameters.
      Tuple<string, string> email = EmailTemplateExtension.Compile(emailTemplate, values);

      this.Send(address, email.Item1, email.Item2);
    }

    /// <summary>
    /// Envoie un email.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="subject">Le sujet de l'email.</param>
    /// <param name="body">Le body de l'email (au format html).</param>
    public void Send(EmailAddress address, string subject, string body)
    {
      if (address == null)
      {
        throw new ArgumentNullException(nameof(address));
      }
      else if (string.IsNullOrEmpty(subject))
      {
        throw new ArgumentNullException(nameof(subject));
      }
      else if (body == null)
      {
        throw new ArgumentNullException(nameof(body));
      }

      MimeMessage message = new MimeMessage();

      // From
      message.From.Add(new MailboxAddress(this.appSettings.Email.From.Name, this.appSettings.Email.From.Address));

      // To
      // TODO: Faire une vérif de la validitée des emails avant.
      if (!string.IsNullOrEmpty(address?.Name))
      {
        message.To.Add(new MailboxAddress(address.Name, address.Address));
      }
      else
      {
        message.To.Add(new MailboxAddress(address.Address));
      }

      // Subject
      message.Subject = subject;

      // Body
      message.Body = new TextPart(TextFormat.Html)
      {
        Text = body,
      };

      if (!this.IsConnected)
      {
        this.TryConnect();
      }

      this.smtpClient.Send(message);
    }

    /// <summary>
    /// Essaie d'envoyer un email.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="subject">Le sujet de l'email.</param>
    /// <param name="body">Le body de l'email (au format html).</param>
    /// <returns>Si l'envoi d'email a reussi ou non.</returns>
    public bool TrySend(EmailAddress address, string subject, string body)
    {
      try
      {
        this.Send(address, subject, body);
        return true;
      }
      catch (SmtpCommandException ex)
      {
        // TODO
        this.Logger.LogCritical(ex, "Error while sending email.");
        return false;
      }
    }

    /// <summary>
    /// Essaie d'envoyer un email, basé sur un template HTML.
    /// </summary>
    /// <param name="address">L'<see cref="EmailAddress"/> de la personne a contacter.</param>
    /// <param name="templateName">Le nom du template email.</param>
    /// <param name="values">Les valeurs à injecter dans le template.</param>
    /// <returns>Si l'envoi d'email a reussi ou non.</returns>
    public bool TrySendTemplate(EmailAddress address, string templateName, dynamic values)
    {
      try
      {
        this.SendTemplate(address, templateName, values);
        return true;
      }
      catch (SmtpCommandException ex)
      {
        this.Logger.LogCritical(ex, "Error while sending email.");
        return false;
      }
    }

    /// <summary>
    /// Dispose les éléments en mémoire de cette instance de <see cref="EmailService"/>.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose les éléments en mémoire de cette instance de <see cref="EmailService"/>.
    /// </summary>
    /// <param name="disposing">Permet de valider le fait de disposer les ressources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.IsConnected)
      {
        this.smtpClient.Disconnect(true);
      }

      this.smtpClient.Dispose();
    }

    /// <summary>
    /// Se connecter au serveur smtp.
    /// </summary>
    private void TryConnect()
    {
      if (!this.IsConnected)
      {
        this.smtpClient.Connect(this.appSettings.Email.Smtp.Host, this.appSettings.Email.Smtp.Port, true);
        this.smtpClient.Authenticate(this.appSettings.Email.Smtp.Username, this.appSettings.Email.Smtp.Password);
      }
    }
  }
}
