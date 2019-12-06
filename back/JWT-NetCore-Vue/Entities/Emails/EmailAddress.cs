namespace JWTNetCoreVue.Entities.Emails
{
  /// <summary>
  /// Classe <see cref="EmailAddress"/>.
  /// Classe permettant de représenter une personne impliquée dans un echange email.
  /// </summary>
  public class EmailAddress
  {
    /// <summary>
    /// Obtient ou définit l'addresse de l'email.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Obtient ou définit le nom de la personne.
    /// </summary>
    public string Name { get; set; }
  }
}
