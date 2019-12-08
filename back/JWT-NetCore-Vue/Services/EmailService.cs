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
  public class EmailService : ALoggedLocalizedService<EmailService>, IEmailService
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    public AppSettings _appSettings;

    /// <summary>
    /// Le service des templates emails.
    /// </summary>
    public IEmailTemplateService _emailTemplateService;


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
