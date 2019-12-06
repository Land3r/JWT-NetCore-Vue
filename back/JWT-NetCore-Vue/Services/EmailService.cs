using JWTNetCoreVue.Services.Core;
using JWTNetCoreVue.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JWTNetCoreVue.Services
{
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

    public EmailService([FromServices]IStringLocalizer<EmailService> localizer,
      IOptions<AppSettings> appSettings,
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
