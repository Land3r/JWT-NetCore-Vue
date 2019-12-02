﻿namespace JWTNetCoreVue.Services
{
  using System;
  using System.Globalization;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using JWTNetCoreVue.Entities;
  using JWTNetCoreVue.Extensions;
  using JWTNetCoreVue.Models;
  using JWTNetCoreVue.Settings;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;
  using MongoDB.Driver;

  /// <summary>
  /// Classe UserService.
  /// Service pour la gestion des utilisateurs.
  /// </summary>
  public class UserService : AMongoEntityService<User>, IUserService
  {
    private const string _collectionName = "Users";
    ///// <summary>
    ///// La collection des utilisateurs en base.
    ///// </summary>
    //private readonly IMongoCollection<User> _users;

    // Utilisateurs hardcodé dans ce cas pour la simplicité
    // TODO: Ajouter une réelle collection d'utilisateurs venant d'une base de données ou autre source de données.
    // FIXME: En production, ne stockez JAMAIS les mots de passes en clair. Utilisez une fonction de hashage ou d'encryption.
    //private readonly List<User> _users = new List<User>
    //{
    //  new User { Id = new Guid(), FirstName = "Test", LastName = "User", Username = "test", Password = "test", Email="ngordat@github.com" }
    //};

    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings _appSettings;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="UserService"/>.
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    public UserService(IOptions<AppSettings> appSettings) : base(appSettings, _collectionName)
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

    /// <summary>
    /// Authentifie un <see cref="User"/>, basé sur le <see cref="UserAuthenticateModel"/> fourni.
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> authentifié.</returns>
    public User Authenticate(UserAuthenticateModel model)
    {
      User user = _entities.Find(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

      if (user == null)
      {
        return null;
      }
      else
      {
        // Generation du token JWT.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.JWT.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString(CultureInfo.InvariantCulture))
          }),
          Expires = DateTime.UtcNow.AddDays(_appSettings.JWT.DurationInDays),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);

        return user.WithoutPassword();
      }
    }

    /// <summary>
    /// Obtient un <see cref="User"/>, basé sur le username fourni.
    /// </summary>
    /// <param name="username">Le nom d'utilisateur à utiliser pour authentifier l'<see cref="Utilisateur"/>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see>.</returns>
    public User Get(string username)
    {
      return _entities.Find(elm => elm.Username == username).FirstOrDefault();
    }
  }
}