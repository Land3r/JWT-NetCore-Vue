namespace JWTNetCoreVue
{
  using System;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using NLog.Web;

  /// <summary>
  /// Classe Program.
  /// Point d'entrée de l'application.
  /// </summary>
  public static class Program
  {
    /// <summary>
    /// Point d'entrée de l'application.
    /// </summary>
    /// <param name="args">Les arguments d'instanciation de l'application.</param>
    public static void Main(string[] args)
    {
      var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      try
      {
        logger.Debug("Application Initialisation.");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception exception)
      {
        // NLog: catch setup errors
        logger.Error(exception, "Impossible to build the host.");
        throw;
      }
      finally
      {
        logger.Info("Application stopped.");
        NLog.LogManager.Shutdown();
      }
    }

    /// <summary>
    /// Crée le host du serveur.
    /// </summary>
    /// <param name="args">Les arguments d'instanciation de l'application.</param>
    /// <returns>Le host à builder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        }).ConfigureLogging(logging =>
        {
          logging.ClearProviders();
        })
      .UseNLog();
    }
  }
}
