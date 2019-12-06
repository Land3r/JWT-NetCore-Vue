namespace JWTNetCoreVue.Settings
{
  /// <summary>
  /// Classe <see cref="EmailFromSettings"/>.
  /// Classe permettant de récupérer les informations du champs "To" de la configuration email.
  /// </summary>
  public class EmailFromSettings
  {
    /// <summary>
    /// Obtient ou définit l'addresse de l'email utilisé pour l'envoi des emails.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Obtient ou définit le nom d'affichage utilisé pour l'envoi des emails.
    /// </summary>
    public string Name { get; set; }
  }
}
