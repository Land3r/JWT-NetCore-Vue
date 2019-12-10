namespace JWTNetCoreVue.Extensions
{
  using System;
  using System.Reflection;
  using JWTNetCoreVue.Entities.Emails;

  /// <summary>
  /// Collection de méthodes d'extensions pour la gestion des <see cref="EmailTemplate">templates emails</see>.
  /// </summary>
  public static class EmailTemplateExtension
  {
    /// <summary>
    /// Compile un template email avec les données spécifiques à cette instance d'email.
    /// </summary>
    /// <param name="emailTemplate">Le template email à utiliser.</param>
    /// <param name="values">Les données de l'email.</param>
    /// <returns>Un <see cref="Tuple{string, string}"/> contenant le résultat compilé du Subject, puis du Body de l'email.</returns>
    public static Tuple<string, string> Compile(this EmailTemplate emailTemplate, dynamic values)
    {
      if (emailTemplate == null)
      {
        throw new ArgumentNullException(nameof(emailTemplate));
      }

      string subjectTemplate = emailTemplate.Subject;
      string bodyTemplate = emailTemplate.Body;

      foreach (var prop in values.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        subjectTemplate = subjectTemplate.Replace($"{{{prop.Name}}}", prop.GetValue(values, null));
        bodyTemplate = bodyTemplate.Replace($"{{{prop.Name}}}", prop.GetValue(values, null));
      }

      return new Tuple<string, string>(subjectTemplate, bodyTemplate);
    }
  }
}
