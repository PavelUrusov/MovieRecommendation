using MovieRecommendation.API.Common.Extensions;

namespace MovieRecommendation.API;

/// <summary>
///     The entry point class for the application.
/// </summary>
public class Program
{
    private static ILogger<Program> _logger = null!;

    /// <summary>
    ///     The main method that starts the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.ConfigureServices().Build();

            _logger = app.Services.GetRequiredService<ILogger<Program>>();

            _logger.LogInformation("Starting up");

            app.ConfigurePipeline();

            app.Run();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unhandled exception");
        }
        finally
        {
            _logger.LogInformation("Shut down complete");
        }
    }
}