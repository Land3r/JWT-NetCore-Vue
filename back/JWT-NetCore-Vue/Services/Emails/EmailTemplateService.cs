namespace JWTNetCoreVue.Services.Emails
{
  using JWTNetCoreVue.Entities.Emails;
  using JWTNetCoreVue.Services.Core;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using MongoDB.Driver;

  /// <summary>
  /// Classe <see cref="EmailTemplateService"/>.
  /// Service CRUD pour les <see cref="EmailTemplate"/>.
  /// </summary>
  public class EmailTemplateService : AMongoEntityLocalizedService<EmailTemplate, EmailTemplateService>, IEmailTemplateService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string CollectionName = "EmailTemplates";

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="EmailTemplateService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public EmailTemplateService(
      [FromServices]IStringLocalizer<EmailTemplateService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<EmailTemplateService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }

    /// <summary>
    /// Obtient un <see cref="EmailTemplate"/> par son nom.
    /// </summary>
    /// <param name="templateName">Le nom du template email.</param>
    /// <returns>Le <see cref="EmailTemplate"/> si trouvé.</returns>
    public EmailTemplate GetByName(string templateName)
    {
      return this.Entities.Find(template => template.Name == templateName).FirstOrDefault();
    }
  }
}
