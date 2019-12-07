namespace JWTNetCoreVue
{
  using System.Text;
  using JWTNetCoreVue.Security;
  using JWTNetCoreVue.Services;
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
    /// Obtient la configuration de l'application.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">La configuration de l'application.</param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      CorsConfiguration.ConfigureServices(services);

      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddMvc()
        .AddViewLocalization(options => options.ResourcesPath = "Resources")
        .AddDataAnnotationsLocalization();

      services.AddControllers();

      // configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.JWT.Secret);
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
          ValidateAudience = false
        };
      });

      // configure DI for application services
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserPasswordResetTokenService, UserPasswordResetTokenService>();
      services.AddScoped<IEmailService, EmailService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
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
  }
}
