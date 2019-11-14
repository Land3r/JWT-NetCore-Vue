namespace JWTNetCoreVue
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;

  /// <summary>
  /// Classe Program.
  /// Point d'entr�e de l'application.
  /// </summary>
  public static class Program
  {

    /// <summary>
    /// Point d'entr�e de l'application.
    /// </summary>
    /// <param name="args">Les arguments d'instanciation de l'application.</param>
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Cr�e le host du serveur.
    /// </summary>
    /// <param name="args">Les arguments d'instanciation de l'application.</param>
    /// <returns>Le host � builder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
    }
  }
}
