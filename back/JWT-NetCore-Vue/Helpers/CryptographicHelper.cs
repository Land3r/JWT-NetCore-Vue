namespace JWTNetCoreVue.Helpers
{
  using System;
  using System.Globalization;
  using System.Linq;
  using System.Security.Cryptography;
  using System.Text;

  /// <summary>
  /// Classe <see cref="CryptographicHelper"/>.
  /// Collection d'helpers pour faciliter l'utilisation de fonctions cryptographiques.
  /// </summary>
  public static class CryptographicHelper
  {
    /// <summary>
    /// L'alphabet utilisé pour convertir en base62.
    /// </summary>
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";

    /// <summary>
    /// Génère un token de la longueur demandée pouvant être transmis via un paramètre GET sans necessiter d'encodage.
    /// </summary>
    /// <param name="length">La longueur du token à générer.</param>
    /// <returns>Le token généré.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1055:Uri return values should not be strings", Justification = "This is not meant to produce Uri.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "Simple design is too concive.")]
    public static string GetUrlSafeToken(int length)
    {
      if (length == 0)
      {
        // TODO
        throw new ArgumentException("Length must be greater than 0", nameof(length));
      }

      using (RandomNumberGenerator rnd = new RNGCryptoServiceProvider())
      {
        byte[] tokenBytes = new byte[length];
        rnd.GetBytes(tokenBytes);
        var token = Enumerable
                      .Range(0, length)
                      .Select(i => Alphabet[tokenBytes[i] % Alphabet.Length])
                      .ToArray();
        return new string(token);
      }
    }

    /// <summary>
    /// Génère un hash d'une valeur.
    /// </summary>
    /// <param name="clear">La valeur en clair à hasher (sous forme binaire).</param>
    /// <returns>La valeur hashée.</returns>
    public static byte[] GetHash(byte[] clear)
    {
      if (clear == null)
      {
        throw new ArgumentNullException(nameof(clear));
      }

      byte[] result;
      using (SHA512 shaM = new SHA512Managed())
      {
        result = shaM.ComputeHash(clear);
      }

      return result;
    }

    /// <summary>
    /// Génère un hash d'une chaine de charactères en UTF8.
    /// </summary>
    /// <param name="clear">La valeur en clair à hasher (sous forme de texte).</param>
    /// <returns>La valeur hashée.</returns>
    public static string GetHash(string clear)
    {
      if (string.IsNullOrEmpty(clear))
      {
        throw new ArgumentNullException(nameof(clear));
      }

      byte[] data = Encoding.UTF8.GetBytes(clear);
      byte[] result = GetHash(data);

      return ByteArrayToString(result);
    }

    /// <summary>
    /// Génère un hash d'une valeur, prefixé par un salt.
    /// </summary>
    /// <param name="clear">La valeur en clair à hasher (sous forme binaire).</param>
    /// <param name="salt">Le salt à utiliser.</param>
    /// <returns>La valeur hashée.</returns>
    public static byte[] GetHash(byte[] clear, byte[] salt)
    {
      byte[] data = clear.Concat(salt).ToArray();
      byte[] result = GetHash(data);

      return result;
    }

    /// <summary>
    /// Génère un hash d'une chaine de charactères en UTF8.
    /// </summary>
    /// <param name="clear">La valeur en clair à hasher (sous forme de texte).</param>
    /// <param name="salt">Le salt à utiliser.</param>
    /// <returns>La valeur hashée.</returns>
    public static string GetHash(string clear, string salt)
    {
      string saltedclear = string.Concat(salt, clear);
      byte[] data = Encoding.UTF8.GetBytes(saltedclear);
      byte[] result = GetHash(data);

      return ByteArrayToString(result);
    }

    /// <summary>
    /// Convertie un tableau de byte en chaine de characteres.
    /// </summary>
    /// <param name="bytes">Le tableau de bytes.</param>
    /// <returns>La chaine de characteres correspondant.</returns>
    private static string ByteArrayToString(byte[] bytes)
    {
      StringBuilder hex = new StringBuilder(bytes.Length * 2);
      foreach (byte b in bytes)
      {
        hex.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", b);
      }

      return hex.ToString();
    }
  }
}
