namespace MovieRecommendation.API.Common.Extensions;

/// <summary>
///     Extension methods for configuring the application pipeline.
/// </summary>
public static class WebApplicationExtension
{
    /// <summary>
    ///     Configures the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication" /> instance.</param>
    /// <returns>The configured <see cref="WebApplication" /> instance.</returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}