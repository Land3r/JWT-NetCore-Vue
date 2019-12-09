namespace JWTNetCoreVue.Services
{
  using JWTNetCoreVue.Entities.Emails;
  using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using System;
  using JWTNetCoreVue.Extensions;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Mail;
  using System.Threading.Tasks;


  /// <summary>
  /// Classe <see cref="EmailService"/>.
  /// Service permettant de gérer les échanges emails de l'application.
  /// </summary>
  public class EmailService : ALoggedLocalizedService<EmailService>, IEmailService, IDisposable
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private AppSettings _appSettings;

    /// <summary>
    /// Le service des templates emails.
    /// </summary>
    public IEmailTemplateService _emailTemplateService;


    /// <summary>
    /// L'instance du client smtp.
    /// </summary>
    private SmtpClient _smtpClient = new SmtpClient();

    /// <summary>
    /// Obtient si le service est connecté au smtp.
    /// </summary>
    private bool IsConnected
    {
      get { return _smtpClient.IsConnected; }
    }

    /// <summary>
    /// Initialise une nouvelle instance de <see cref="EmailService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources localisées.</param>
    /// <param name="appSettings">La configuration de l'application</param>
    /// <param name="logger">Le logger utilisé.</param>
    public EmailService([FromServices]IStringLocalizer<EmailService> localizer,
      IOptions<AppSettings> appSettings,
      IEmailTemplateService emailTemplateService,
      [FromServices] ILogger<EmailService> logger) : base(logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        _appSettings = appSettings.Value;
      }

      if (emailTemplateService == null)
      {
        throw new ArgumentNullException(nameof(emailTemplateService));
      }
      else
      {
        _emailTemplateService = emailTemplateService;
      }
    }

    //>TODO: Remplacer address par le type concret.
    private bool Send(string address, string subject, string body)
    {
      //TODO: Remplacer par le code.

      return true;
    }

    public bool SendTemplate(string address, string templateName, dynamic values)
    {
      if (string.IsNullOrEmpty(templateName))
      {
        throw new ArgumentNullException(nameof(templateName));
      }

      EmailTemplate emailTemplate = _emailTemplateService.GetByName(templateName);
      if (emailTemplate == null)
      {
        throw new ApplicationException($"EmailTemplate nammed {templateName} not found.");
      }

      // We need to call explicitly the extension method because it uses dynamic type parameters.
      Tuple<string, string> email = EmailTemplateExtension.Compile(emailTemplate, values);

      return this.Send(address, email.Item1, email.Item2);
    }

    /// <summary>
    /// Desctructeur de l'instance de la classe.
    /// </summary>
    ~EmailService()
    {
      Dispose(false);
    }

    /// <summary>
    /// Se connecter au serveur smtp.
    /// </summary>
    private void TryConnect()
    {
      if (!IsConnected)
      {
        _smtpClient.Connect(_appSettings.Email.Smtp.Host, _appSettings.Email.Smtp.Port, true);
        _smtpClient.Authenticate(_appSettings.Email.Smtp.Username, _appSettings.Email.Smtp.Password);
      }
    }

    /// <summary>
    /// Envoie un email
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
      message.From.Add(new MailboxAddress(_appSettings.Email.From.Name, _appSettings.Email.From.Address));
      // Reply-to
      //message.ReplyTo.Add(new MailboxAddress(_appSettings.Email.From.Name, _appSettings.Email.From.Address));
      // To
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
        Text = body
      };

      if (!IsConnected)
      {
        TryConnect();
      }
      _smtpClient.Send(message);
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
        Send(address, subject, body);
        return true;
      }
      catch (Exception ex)
      {
        Logger.LogCritical(ex, "Error while sending email.");
        return false;
      }
    }

    #region Implementation de l'interface IDisposable

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (IsConnected)
      {
        _smtpClient.Disconnect(true);
      }
      _smtpClient.Dispose();
    }

    #endregion Implementation de l'interface IDisposable

    //}
    //// TODO: Ajouter des méthodes d'ajouts d'email.
    //public void SendTest()
    //{
    //  var fromAddress = new MailAddress();
    //  var toAddress = new MailAddress();
    //  const string fromPassword = "";
    //  const string subject = "Coucou";
    //  const string body = "<h1>COUCOU</h1><p>Tu veux un peu de crème pour les mains ??</p>";

    //  var smtp = new SmtpClient
    //  {
    //    Host = ,
    //    Port = ,
    //    EnableSsl = true,
    //    DeliveryMethod = SmtpDeliveryMethod.Network,
    //    UseDefaultCredentials = false,
    //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //  };
    //  using (var message = new MailMessage(fromAddress, toAddress)
    //  {
    //    Subject = subject,
    //    Body = body
    //  })
    //  {
    //    smtp.Send(message);
    //  }
    //}
  }
}
