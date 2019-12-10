namespace JWTNetCoreVue
{
  using System.Text;
  using JWTNetCoreVue.Security;
  using JWTNetCoreVue.Services.Emails;
  using JWTNetCoreVue.Services.Users;
  using JWTNetCoreVue.Settings;
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.HttpOverrides;
  using Microsoft.AspNetCore.Mvc.Razor;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.IdentityModel.Tokens;

  /// <summary>
  /// Classe Startup
  /// Classe permettant la configuration du projet.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">La configuration de l'application.</param>
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    /// <summary>
    /// Obtient la configuration de l'application.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Cette méthode est appelée par le runtime. Elle sert à configurer le traitement des requêtes HTTP.
    /// </summary>
    /// <param name="app">Le builder d'application.</param>
    /// <param name="env">Le builder de l'hôte web.</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      CorsConfiguration.Configure(app, env);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // Necessary for production build.
      // Allows the application to be delivered through a reverse proxy setup (with nginx and kestrel typically).
      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    /// <summary>
    /// Cette méthode est appellé par le runtime. Elle sert à ajouter des service au conteneur.
    /// </summary>
    /// <param name="services">La collection des services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      CorsConfiguration.ConfigureServices(services);

      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddMvc()
        .AddViewLocalization(options => options.ResourcesPath = "Resources")
        .AddDataAnnotationsLocalization();

      services.AddControllers();

      // configure strongly typed settings objects
      var appSettingsSection = this.Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Security.JWT.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });

      // configure DI for application services
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserPasswordResetTokenService, UserPasswordResetTokenService>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<IEmailTemplateService, EmailTemplateService>();
    }
  }
}
