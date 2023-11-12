using Microsoft.Extensions.Logging;

namespace MovieRecommendation.TrainingModel.Common.Helpers;

internal static class LoggerHelper
{
    internal static ILogger<T> CreateLogger<T>()
    {
        var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        var logger = new Logger<T>(loggerFactory);

        return logger;
    }
}