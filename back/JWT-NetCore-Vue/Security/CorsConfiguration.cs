namespace JWTNetCoreVue.Security
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  /// <summary>
  /// Espace de configuration pour Cors.
  /// </summary>
  public static class CorsConfiguration
  {
    /// <summary>
    /// Policy Cors pour tout autoriser.
    /// </summary>
    public static readonly string CorsPolicyAllowAll = "CorsAllowAll";

    /// <summary>
    /// Policy Cors pour autoriser seulement certains origines et méthodes.
    /// </summary>
    public static readonly string CorsPolicyFrontend = "CorsFrontend";

    /// <summary>
    /// Autre Policy Cors pour monter l'application de plusieures policy.
    /// </summary>
    public static readonly string CorsPolicyOther = "CorsOther";

    /// <summary>
    /// Liste des policy Cors activées pour le mode production.
    /// </summary>
    private static readonly string[] ProductionCorsPolicies = { CorsPolicyFrontend, CorsPolicyOther };

    /// <summary>
    /// Liste des policy Cors activées pour le mode dévelopement.
    /// </summary>
    private static readonly string[] DevelopmentCorsPolicies = { CorsPolicyAllowAll };

    /// <summary>
    /// Configure les les différents politiques Cors qui peuvent être mise en place.
    /// </summary>
    /// <param name="services">La collection de services utilisées par l'application.</param>
    internal static void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyAllowAll,
          builder =>
          {
            builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyFrontend,
          builder =>
          {
            builder.WithOrigins("http://*.ngordat.net", "https://*.ngordat.net")
              .AllowAnyHeader()
              .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete, HttpMethods.Options)
              .AllowCredentials()
              .SetIsOriginAllowedToAllowWildcardSubdomains();
          });
      });

      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyOther,
          builder =>
          {
            builder.WithOrigins("www.other-url.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
          });
      });
    }

    /// <summary>
    /// Configure les politiques d'accès Cors pour l'application, en fonction de l'environement d'execution.
    /// </summary>
    /// <param name="app">Le builder de l'application.</param>
    /// <param name="env">Le builder de l'environement.</param>
    internal static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        foreach (string corsPolicy in DevelopmentCorsPolicies)
        {
          app.UseCors(corsPolicy);
        }
      }
      else
      {
        foreach (string corsPolicy in ProductionCorsPolicies)
        {
          app.UseCors(corsPolicy);
        }
      }
    }
  }
}
