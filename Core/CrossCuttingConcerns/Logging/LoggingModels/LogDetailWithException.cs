namespace Core.CrossCuttingConcerns.Logging.LoggingModels;

public class LogDetailWithException : LogDetail
{
    public string ExceptionMessage { get; set; }
}
