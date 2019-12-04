using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTNetCoreVue.Services
{
  public class EmailService : IEmailService
  {
    // TODO: Ajouter des méthodes d'ajouts d'email.
    public void SendTest()
    {
      MimeMessage mail = new MimeMessage();

      MailboxAddress from = new MailboxAddress("nicolas.gordat@gmail.com");
      mail.From.Add(from);

      MailboxAddress to = new MailboxAddress("ngordat@sii.fr");
      mail.To.Add(to);

      mail.Subject = "Coucou";

      BodyBuilder builder = new BodyBuilder();
      builder.HtmlBody = "<h1>COUCOU</h1><p>Tu veux un peu de crème pour les mains ??</p>";
      mail.Body = builder.ToMessageBody();

      using (SmtpClient client = new SmtpClient())
      {
        client.Connect("smtp.gmail.com", 587);
        client.Authenticate("xxx", "xxx");
        client.Send(mail);
        client.Disconnect(true);
      }
    }
  }
}
