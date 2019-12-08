namespace JWTNetCoreVue.Services
{
  /// <summary>
  /// Interface <see cref="IEmailService"/>.
  /// Interface permettant d'intéragir avec le service email.
  /// </summary>
  public interface IEmailService
  {
    public bool SendTemplate(string address, string templateName, dynamic values);
  }
}