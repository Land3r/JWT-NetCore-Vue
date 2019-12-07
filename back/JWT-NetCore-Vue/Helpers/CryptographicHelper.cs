using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace JWTNetCoreVue.Helpers
{
  public static class CryptographicHelper
  {
    /// <summary>
    /// L'alphabet utilisé pour convertir en base32 (techniquement base61 ici, car des problèmes d'interpretation du '.' peuvent se produire selon les clients emails.
    /// </summary>
    private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";

    /// <summary>
    /// Génére un token de la longueur demandée pouvant être transmis via un paramètre GET sans necessiter d'encodage.
    /// </summary>
    /// <param name="length">La longueur du token à générer</param>
    /// <returns>Le token généré.</returns>
    public static string GetUrlSafeToken(int length)
    {
      if (length == 0)
      {
        throw new ArgumentException("Length must be greater than 0", nameof(length));
      }

      using (RandomNumberGenerator rnd = new RNGCryptoServiceProvider())
      {
        byte[] tokenBytes = new byte[length];
        rnd.GetBytes(tokenBytes);
        var token = Enumerable
                      .Range(0, length)
                      .Select(i => _alphabet[tokenBytes[i] % _alphabet.Length])
                      .ToArray();
        return new String(token);
      }
    }
  }
}
