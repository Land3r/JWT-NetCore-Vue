namespace JWTNetCoreVue.Services.Emails
{
  using JWTNetCoreVue.Entities.Emails;
  using JWTNetCoreVue.Services.Core;

  /// <summary>
  /// Interface <see cref="IEmailTemplateService"/>.
  /// Interfaces permettant d'intéragir de manière CRUD avec les <see cref="EmailTemplate"/>.
  /// </summary>
  public interface IEmailTemplateService : ICrudService<EmailTemplate>
  {
    /// <summary>
    /// Obtient l'<see cref="EmailTemplate"/> par son nom.
    /// </summary>
    /// <param name="templateName">Le nom du template email.</param>
    /// <returns>Le <see cref="EmailTemplate"/>, si trouvé.</returns>
    EmailTemplate GetByName(string templateName);
  }
}